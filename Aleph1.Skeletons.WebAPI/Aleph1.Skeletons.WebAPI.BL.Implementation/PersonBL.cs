using System;
using System.Collections.Generic;
using System.Linq;

using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.BL.Contracts;
using Aleph1.Skeletons.WebAPI.DAL.Contracts;
using Aleph1.Skeletons.WebAPI.Models.Entities;

namespace Aleph1.Skeletons.WebAPI.BL.Implementation
{
	internal class PersonBL : IPersonBL
	{
		private readonly IGenericRepo Repo;

		public PersonBL(IGenericRepo repo) => Repo = repo;

		public void Dispose() => Repo.Dispose();

		[Logged]
		public IEnumerable<Person> GetPersons() => Repo.GetAll<Person>().ToList();

		[Logged]
		public int GetPersonsCount() => Repo.GetAll<Person>().Count();

		[Logged]
		public Person GetPersonById(int Id) => Repo.GetById<Person>(Id);

		[Logged]
		public IEnumerable<Person> SearchByName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentNullException(nameof(name));
			};

			return Repo
				.GetAll<Person>()
				.Where(person => person.FirstName.Contains(name) || person.LastName.Contains(name))
				.ToList();
		}

		[Logged]
		public Person InsertPerson(Person person)
		{
			Person target = Repo.Insert(person);
			Repo.SaveChanges();
			return target;
		}

		[Logged]
		public Person UpdatePerson(int Id, Person person)
		{
			Person target = Repo.GetById<Person>(Id);

			if (target != default)
			{
				target.FirstName = person.FirstName;
				target.LastName = person.LastName;
				target.Birthdate = person.Birthdate;
				Repo.SaveChanges();
			}

			return target;
		}

		[Logged]
		public Person DeletePerson(int Id)
		{
			Person target = Repo.Delete<Person>(Id);
			Repo.SaveChanges();
			return target;
		}
	}
}
