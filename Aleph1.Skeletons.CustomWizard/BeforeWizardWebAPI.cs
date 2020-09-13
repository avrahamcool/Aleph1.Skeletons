using EnvDTE;

using EnvDTE80;

using Microsoft.VisualStudio.TemplateWizard;

using System.Collections.Generic;
using System.IO;

namespace Aleph1.Skeletons.CustomWizard
{
    public class BeforeWizardWebAPI : IWizard
    {
        private Solution2 solution;
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            // Close new solution
            solution = ((Solution2)((DTE)automationObject).Solution);
            solution.Close();

            // Delete old directory(in my case VS creating it) and change destination
            string oldDestinationDirectory = replacementsDictionary["$destinationdirectory$"];
            if (Directory.Exists(oldDestinationDirectory))
            {
                Directory.Delete(oldDestinationDirectory, true);
            }

            string newDestinationDirectory = Path.Combine($"{oldDestinationDirectory}", @"..\");
            replacementsDictionary["$destinationdirectory$"] = Path.GetFullPath(newDestinationDirectory);
        }
        public void RunFinished()
        {
            solution.Close();
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