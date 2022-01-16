using System;
using System.Collections.Generic;

using Aleph1.Skeletons.WebAPI.Models.Entities;

namespace Aleph1.Skeletons.WebAPI.BL.Contracts
{
    /// <summary>Handles business logic</summary>
    public interface IPersonBL : IDisposable
    {
        /// <summary>get the number of persons currently saved in our system</summary>
        /// <returns>the number of persons in our system</returns>
        int GetPersonsCount();

        /// <summary>get a list of all persons</summary>
        /// <returns>all persons</returns>
        IEnumerable<Person> GetPersons();

        /// <summary>get person by ID</summary>
        /// <param name="Id">the ID of the person</param>
        /// <returns>the person</returns>
        Person GetPersonById(int Id);

        /// <summary>get a list of persons with a name including the search term</summary>
        /// <param name="searchTerm">a string to query against the persons full name</param>
        /// <returns>the persons that include the given query in their name</returns>
        IEnumerable<Person> SearchByName(string searchTerm);

        /// <summary>Insert a new person</summary>
        /// <param name="personToAdd">the person</param>
        /// <returns>the person</returns>
        Person InsertPerson(Person personToAdd);

        /// <summary>update a person</summary>
        /// <param name="Id">the ID of the person</param>
        /// <param name="personToUpdate">the person to update</param>
        /// <returns>the updated person</returns>
        Person UpdatePerson(int Id, Person personToUpdate);

        /// <summary>delete a person by ID</summary>
        /// <param ID="personToDelete">the ID of the person to delete</param>
        /// <returns>the deleted person</returns>
        Person DeletePerson(int Id);
    }
}
