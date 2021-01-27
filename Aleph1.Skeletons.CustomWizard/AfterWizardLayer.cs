using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using EnvDTE;

using EnvDTE80;

using Microsoft.VisualStudio.TemplateWizard;

namespace Aleph1.Skeletons.CustomWizard
{
	public class AfterWizardLayer : IWizard
	{
		private DTE2 dte;
		private Solution2 solution;
		private string selectedFolderName;
		private string pathToNewSolution;
		private string pathToOldSolution;

		private string webApiUniqueID;
		private string[] newProjectsUniqueIds;
		private string[] newProjectsPathToAdd;

		public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
		{
			string NameToUniqueName(string projectPrefix, string projectName)
			{
				return $@"{projectPrefix}.{projectName}\{projectPrefix}.{projectName}.csproj";
			}

			dte = (DTE2)automationObject;
			solution = (Solution2)(dte.Solution);

			string solutionPrefix = replacementsDictionary["$specifiedsolutionname$"];
			string destinationDirectory = replacementsDictionary["$destinationdirectory$"];
			string newProjectsPrefix = replacementsDictionary["$safeprojectname$"];

			webApiUniqueID = NameToUniqueName(solutionPrefix, "WebAPI");

			pathToNewSolution = Path.Combine(destinationDirectory, newProjectsPrefix + ".sln");
			pathToOldSolution = Directory.EnumerateFiles(destinationDirectory, "*.sln").FirstOrDefault(path => path != pathToNewSolution);

			//if a folder is selected - we will add the project to that folder
			selectedFolderName = ((Array)dte.ActiveSolutionProjects).Cast<Project>().Select(p => p.Name).FirstOrDefault();
			if (selectedFolderName != null)
			{
				// Closing the solution will cause the projects to not get added
				solution.SaveAs(pathToOldSolution);
				solution.Close();

				string[] newLayerProjects = new[] {
					"Contracts",
					"Implementation",
					"Mock"
				};

				newProjectsUniqueIds = newLayerProjects.Select(name => NameToUniqueName(newProjectsPrefix, name)).ToArray();
				newProjectsPathToAdd = newProjectsUniqueIds.Select(uniqueId => Path.Combine(destinationDirectory, uniqueId)).ToArray();
			}
		}
		public void RunFinished()
		{
			if (File.Exists(pathToNewSolution))
			{
				File.Delete(pathToNewSolution);
			}

			//if the selected folder exist - we will add the project manually
			if (selectedFolderName != null)
			{
				solution.Open(pathToOldSolution);
				Project selectedProj = solution.FindProjectInSolution(selectedFolderName);
				if (selectedProj != null)
				{
					SolutionFolder selectedFolder = (SolutionFolder)(selectedProj.Object);
					foreach (string projPath in newProjectsPathToAdd)
					{
						selectedFolder.AddFromFile(projPath);
					}
				}
			}

			BuildDependency webApiDependencies = solution.SolutionBuild.BuildDependencies.Item(webApiUniqueID);
			if (webApiDependencies != default)
			{
				foreach (string projUniqueID in newProjectsUniqueIds)
				{
					webApiDependencies.AddProject(projUniqueID);
				}
			}
		}

		public void BeforeOpeningFile(ProjectItem projectItem)
		{
		}

		public void ProjectFinishedGenerating(Project project)
		{
		}

		public void ProjectItemFinishedGenerating(ProjectItem projectItem)
		{
		}

		public bool ShouldAddProjectItem(string filePath)
		{
			return true;
		}
	}
}