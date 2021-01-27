using System.Collections.Generic;
using System.IO;

using EnvDTE;

using Microsoft.VisualStudio.TemplateWizard;

namespace Aleph1.Skeletons.CustomWizard
{
	public class BeforeWizardLayer : IWizard
	{
		public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
		{
			// Delete old directory(in my case VS creating it) and change destination
			string oldDestinationDirectory = replacementsDictionary["$destinationdirectory$"];
			if (Directory.Exists(oldDestinationDirectory))
			{
				Directory.Delete(oldDestinationDirectory, true);
			}

			string newDestinationDirectory = Path.Combine($"{oldDestinationDirectory}", @"..\");
			replacementsDictionary["$destinationdirectory$"] = Path.GetFullPath(newDestinationDirectory);

			string solutionName = replacementsDictionary["$specifiedsolutionname$"];
			if (!replacementsDictionary["$safeprojectname$"].StartsWith(solutionName + "."))
			{
				replacementsDictionary["$projectname$"] = solutionName + "." + replacementsDictionary["$projectname$"];
				replacementsDictionary["$safeprojectname$"] = solutionName + "." + replacementsDictionary["$safeprojectname$"];
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

		public void RunFinished()
		{
		}

		public bool ShouldAddProjectItem(string filePath)
		{
			return true;
		}
	}
}