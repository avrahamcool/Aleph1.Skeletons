using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System.Collections.Generic;
using System.IO;

namespace Aleph1.Skeletons.CustomWizard
{
    public class BeforeWizardLayer : IWizard
    {
        private DTE dte;
        private Dictionary<string, string> replacementsDictionary;
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            dte = (DTE)automationObject;
            this.replacementsDictionary = replacementsDictionary;

            // Delete old directory(in my case VS creating it) and change destination
            string oldDestinationDirectory = replacementsDictionary["$destinationdirectory$"];
            if (Directory.Exists(oldDestinationDirectory))
            {
                Directory.Delete(oldDestinationDirectory, true);
            }

            string newDestinationDirectory = Path.Combine($"{oldDestinationDirectory}", @"..\");
            replacementsDictionary["$destinationdirectory$"] = Path.GetFullPath(newDestinationDirectory);

            string projName = replacementsDictionary["$projectname$"];
            string safeProjName = replacementsDictionary["$safeprojectname$"];
            string solutionName = Path.GetFileName(Path.GetDirectoryName(replacementsDictionary["$solutiondirectory$"]));
            if (!replacementsDictionary["$safeprojectname$"].StartsWith(solutionName))
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