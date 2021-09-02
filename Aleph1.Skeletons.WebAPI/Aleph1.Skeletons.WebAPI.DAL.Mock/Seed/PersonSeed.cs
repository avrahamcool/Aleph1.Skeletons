using System;
using System.Collections.Generic;

using Aleph1.Skeletons.WebAPI.Models.Entities;

namespace Aleph1.Skeletons.WebAPI.DAL.Mock.Seed
{
	internal class PersonSeed : ISeed
	{
		public void Seed(GenericContextMock context)
		{
			List<Person> seed = new()
			{
				new Person() { FirstName = "John", LastName = "Doe", Birthdate = DateTimeOffset.Now },
				new Person() { FirstName = "Jane", LastName = "Roe", Birthdate = new DateTimeOffset(1989, 1, 1, 0, 0, 0, TimeSpan.Zero) }
			};
			context.AddRange(seed);
		}
	}
}
