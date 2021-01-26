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
		private List<string> projectsToAdd;

		public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
		{
			dte = (DTE2)automationObject;
			solution = (Solution2)(dte.Solution);

			string destinationDirectory = replacementsDictionary["$destinationdirectory$"];
			string solutionName = replacementsDictionary["$safeprojectname$"];

			pathToNewSolution = Path.Combine(destinationDirectory, solutionName + ".sln");
			pathToOldSolution = Directory.EnumerateFiles(destinationDirectory, "*.sln").FirstOrDefault(path => path != pathToNewSolution);

			//if a folder is selected - we will add the project to that folder
			selectedFolderName = ((Array)dte.ActiveSolutionProjects).Cast<Project>().Select(p => p.Name).FirstOrDefault();
			if (selectedFolderName != null)
			{
				// Closing the solution will cause the projects to not get added
				solution.SaveAs(pathToOldSolution);
				solution.Close();

				projectsToAdd = new List<string>()
				{
					Path.Combine(destinationDirectory, solutionName + ".Contracts", solutionName + ".Contracts.csproj"),
					Path.Combine(destinationDirectory, solutionName + ".Implementation", solutionName + ".Implementation.csproj"),
					Path.Combine(destinationDirectory, solutionName + ".Mock", solutionName + ".Mock.csproj")
				};
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
					foreach (string projPath in projectsToAdd)
					{
						selectedFolder.AddFromFile(projPath);
					}
				}
			}
			BuildDependencies dep = solution.SolutionBuild.BuildDependencies;
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