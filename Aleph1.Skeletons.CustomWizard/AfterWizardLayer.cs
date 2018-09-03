using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aleph1.Skeletons.CustomWizard
{
    public class AfterWizardLayer : IWizard
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
            string pathToNewSolution = Path.Combine(destinationDirectory, solutionName + ".sln");
            string pathToOldSolution = Directory.EnumerateFiles(destinationDirectory, "*.sln").FirstOrDefault(path => path != pathToNewSolution);

            if (pathToOldSolution != null)
            {
                File.Delete(pathToNewSolution);

                dte.Solution.Open(pathToOldSolution);

                dte.Solution.AddFromFile(Path.Combine(destinationDirectory, solutionName + ".Contracts", solutionName + ".Contracts.csproj"));
                dte.Solution.AddFromFile(Path.Combine(destinationDirectory, solutionName + ".Implementation", solutionName + ".Implementation.csproj"));
                dte.Solution.AddFromFile(Path.Combine(destinationDirectory, solutionName + ".Moq", solutionName + ".Moq.csproj"));
            }
            else
            {
                // Open newly created solution
                dte.Solution.Open(pathToNewSolution);
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