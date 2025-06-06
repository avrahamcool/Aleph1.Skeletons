using System;
using System.Collections.Generic;
using System.Linq;

using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.BL.Contracts;
using Aleph1.Skeletons.WebAPI.DAL.Contracts;
using Aleph1.Skeletons.WebAPI.Models.Entities;

namespace Aleph1.Skeletons.WebAPI.BL.Implementation
{
	internal sealed class PersonBL(IGenericRepo repo) : IPersonBL
	{
		public void Dispose()
		{
			repo.Dispose();
		}

		[Logged]
		public IEnumerable<Person> GetPersons()
		{
			return repo
				.GetAll<Person>()
				.ToList();
		}

		[Logged]
		public int GetPersonsCount()
		{
			return repo
				.GetAll<Person>()
				.Count();
		}

		[Logged]
		public Person GetPersonById(int ID)
		{
			return repo.GetById<Person>(ID);
		}

		[Logged]
		public IEnumerable<Person> SearchByName(string searchTerm)
		{
			if (string.IsNullOrWhiteSpace(searchTerm))
			{
				throw new ArgumentNullException(nameof(searchTerm));
			}
			;

			return repo
				.GetAll<Person>()
				.Where(p => p.FirstName.Contains(searchTerm) || p.LastName.Contains(searchTerm))
				.ToList();
		}

		[Logged]
		public Person InsertPerson(Person person)
		{
			Person added = repo.Insert(person);
			repo.SaveChanges();
			return added;
		}

		[Logged]
		public Person UpdatePerson(int Id, Person personToUpdate)
		{
			Person target = repo.GetById<Person>(Id);
			if (target != default)
			{
				target.FirstName = personToUpdate.FirstName;
				target.LastName = personToUpdate.LastName;
				target.BirthDate = personToUpdate.BirthDate;

				repo.SaveChanges();
			}

			return target;
		}

		[Logged]
		public Person DeletePerson(int Id)
		{
			Person deleted = repo.Delete<Person>(Id);
			repo.SaveChanges();
			return deleted;
		}
	}
}
