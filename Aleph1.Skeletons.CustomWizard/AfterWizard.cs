using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System.Collections.Generic;
using System.IO;

namespace Aleph1.Skeletons.CustomWizard
{
    public class AfterWizard : IWizard
    {
        private DTE dte;
        private string destinationDirectory;
        private string solutionName;

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            dte = (DTE)automationObject;
            destinationDirectory = replacementsDictionary["$destinationdirectory$"];
            solutionName = replacementsDictionary["$safeprojectname$"];
        }

        public void RunFinished()
        {
            // Open "old" solution
            var solution = dte.Solution;
            var pathToOldSolution = Path.Combine(destinationDirectory, solutionName + ".sln");
            solution.Open(pathToOldSolution);
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