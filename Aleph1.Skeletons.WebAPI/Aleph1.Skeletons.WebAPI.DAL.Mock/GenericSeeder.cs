using System;
using System.Collections.Generic;
using System.Linq;

namespace Aleph1.Skeletons.WebAPI.DAL.Mock
{
	internal static class GenericSeeder
	{
		internal static void SeedAll()
		{
			using GenericContextMock context = new(SettingsManager.DBOptions);

			Type seederType = typeof(ISeed);

			IEnumerable<Type> allSeeders = seederType
				.Assembly
				.GetTypes()
				.Where(someClass => someClass.IsClass && seederType.IsAssignableFrom(someClass));

			foreach (Type seeder in allSeeders)
			{
				ISeed instance = Activator.CreateInstance(seeder) as ISeed;
				instance.Seed(context);
			}

			context.SaveChanges();
		}
	}
}
