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

		public void Dispose()
		{
			Repo.Dispose();
		}

		[Logged]
		public IEnumerable<Person> GetPersons()
		{
			return Repo
				.GetAll<Person>()
				.ToList();
		}

		[Logged]
		public int GetPersonsCount()
		{
			return Repo
				.GetAll<Person>()
				.Count();
		}

		[Logged]
		public Person GetPersonById(int ID)
		{
			return Repo.GetById<Person>(ID);
		}

		[Logged]
		public IEnumerable<Person> SearchByName(string searchTerm)
		{
			if (string.IsNullOrWhiteSpace(searchTerm))
			{
				throw new ArgumentNullException(nameof(searchTerm));
			};

			return Repo
				.GetAll<Person>()
				.Where(p => p.FirstName.Contains(searchTerm) || p.LastName.Contains(searchTerm))
				.ToList();
		}

		[Logged]
		public Person InsertPerson(Person person)
		{
			Person added = Repo.Insert(person);
			Repo.SaveChanges();
			return added;
		}

		[Logged]
		public Person UpdatePerson(int Id, Person personToUpdate)
		{
			Person target = Repo.GetById<Person>(Id);
			if (target != default)
			{
				target.FirstName = personToUpdate.FirstName;
				target.LastName = personToUpdate.LastName;
				target.BirthDate = personToUpdate.BirthDate;

				Repo.SaveChanges();
			}

			return target;
		}

		[Logged]
		public Person DeletePerson(int Id)
		{
			Person deleted = Repo.Delete<Person>(Id);
			Repo.SaveChanges();
			return deleted;
		}
	}
}
