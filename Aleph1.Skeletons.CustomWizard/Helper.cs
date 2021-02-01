using System;
using System.Collections.Generic;

using EnvDTE;

using EnvDTE80;

namespace Aleph1.Skeletons.CustomWizard
{
	internal static class Helper
	{
		public static void EnrichTemplateVariables(this Dictionary<string, string> replacementsDictionary)
		{
			// need to add passthrough in order to work

			replacementsDictionary.Add("passthrough:TemplateAuthors", replacementsDictionary["$username$"]);
			//replacementsDictionary.Add("passthrough:TemplateCompany", "???");
			replacementsDictionary.Add("passthrough:TemplateYear", replacementsDictionary["$year$"]);
		}
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
