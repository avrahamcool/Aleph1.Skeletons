using EnvDTE;
using EnvDTE80;
using System;

namespace Aleph1.Skeletons.CustomWizard
{
    internal static class Helper
    {
        public static Project NavigateProject(this Project p, string projectName)
        {
            if (p.Name.Equals(projectName, StringComparison.OrdinalIgnoreCase))
            {
                return p;
            }

            if (p.ConfigurationManager == null)
            {
                ProjectItems projectItems = p.ProjectItems;
                if (projectItems != null)
                {
                    foreach (ProjectItem projectItem in projectItems)
                    {
                        if (projectItem.SubProject != null)
                        {
                            Project project = projectItem.SubProject.NavigateProject(projectName);
                            if (project != null)
                            {
                                return project;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public static Project FindProjectInSolution(this Solution2 solution, string projectName)
        {
            foreach (Project project1 in solution.Projects)
            {
                Project project2 = project1.NavigateProject(projectName);
                if (project2 != null)
                {
                    return project2;
                }
            }

            return null;
        }
    }
}
