using System;
using System.Collections.Generic;

using Aleph1.Skeletons.WebAPI.DAL.Implementation;
using Aleph1.Skeletons.WebAPI.Models.Entities;

namespace Aleph1.Skeletons.WebAPI.DAL.Mock.Seed
{
	internal sealed class PersonSeed : ISeed
	{
		public void Seed(GenericContext context)
		{
			List<Person> seed =
			[
				new Person()
				{
					FirstName = "Avraham",
					LastName = "Essoudry",
					BirthDate = DateTimeOffset.Now
				},
				new Person()
				{
					FirstName = "Some",
					LastName = "Body",
					BirthDate = new DateTimeOffset(1989, 1, 1, 0, 0, 0, TimeSpan.Zero)
				}
			];
			context.AddRange(seed);
		}
	}
}
