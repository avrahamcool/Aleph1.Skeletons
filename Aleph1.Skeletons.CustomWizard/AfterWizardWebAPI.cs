using EnvDTE;

using EnvDTE80;

using Microsoft.VisualStudio.TemplateWizard;

using System.Collections.Generic;
using System.IO;

namespace Aleph1.Skeletons.CustomWizard
{
    public class AfterWizardWebAPI : IWizard
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
            string pathToOldSolution = Path.Combine(destinationDirectory, solutionName + ".sln");

            Solution2 solution = ((Solution2)dte.Solution);
            solution.Open(pathToOldSolution);

            solution.Properties.Item("StartupProject").Value = solutionName + ".WebAPI";
            solution.SolutionBuild.Clean(true);
            solution.SolutionBuild.Build();
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