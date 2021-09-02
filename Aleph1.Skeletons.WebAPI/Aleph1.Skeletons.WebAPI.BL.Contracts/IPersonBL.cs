using System;
using System.Collections.Generic;

using Aleph1.Skeletons.WebAPI.Models.Entities;

namespace Aleph1.Skeletons.WebAPI.BL.Contracts
{
	/// <summary>Person business logic layer</summary>
	public interface IPersonBL : IDisposable
	{
		/// <summary>Get the number of persons currently saved in the system</summary>
		/// <returns>Number of persons</returns>
		int GetPersonsCount();

		/// <summary>Get a list of all persons</summary>
		/// <returns>List of persons</returns>
		IEnumerable<Person> GetPersons();

		/// <summary>Gets a single person by identification number</summary>
		/// <param name="Id">Unique identification number</param>
		/// <returns>A single person</returns>
		Person GetPersonById(int Id);

		/// <summary>Find persons whose name includes the search query</summary>
		/// <param name="name">Search query string</param>
		/// <returns>List of persons</returns>
		IEnumerable<Person> SearchByName(string name);

		/// <summary>Insert a new person</summary>
		/// <param name="person">Person to be inserted</param>
		/// <returns>Inserted person</returns>
		Person InsertPerson(Person person);

		/// <summary>Update a person's details</summary>
		/// <param name="Id">Selected person identification number</param>
		/// <param name="person">Person to be updated</param>
		/// <returns>Updated person</returns>
		Person UpdatePerson(int Id, Person person);

		/// <summary>Delete a person by their identification number</summary>
		/// <param name="Id">Selected person identification number</param>
		/// <returns>Deleted person</returns>
		Person DeletePerson(int Id);
	}
}
