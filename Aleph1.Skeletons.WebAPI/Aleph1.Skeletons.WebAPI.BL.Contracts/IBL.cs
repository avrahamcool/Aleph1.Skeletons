using Aleph1.Skeletons.WebAPI.Models;

using System;
using System.Linq;

namespace Aleph1.Skeletons.WebAPI.BL.Contracts
{
    /// <summary>Handles business logic</summary>
    public interface IBL : IDisposable
    {
        /// <summary>get the number of persons currently saved in our system</summary>
        /// <returns>the number of persons in our system</returns>
        int GetPersonsCount();

        /// <summary>get a list of all persons</summary>
        /// <returns>all persons</returns>
        IQueryable<Person> GetPersons();

        /// <summary>get person by ID</summary>
        /// <param name="ID">the ID of the person</param>
        /// <returns>the person</returns>
        Person GetPersonByID(int ID);

        /// <summary>get a list of persons with a name inclusding the search term</summary>
        /// <param name="searchTerm">a string to query against the persons full name</param>
        /// <returns>the persons that include the given query in their name</returns>
        IQueryable<Person> SearchByName(string searchTerm);

        /// <summary>Insert a new person</summary>
        /// <param name="personToAdd">the person</param>
        /// <returns>the person</returns>
        Person InsertPerson(Person personToAdd);

        /// <summary>update a person</summary>
        /// <param name="ID">the ID of the person</param>
        /// <param name="personToUpdate">the person to update</param>
        /// <returns>the updated person</returns>
        Person UpdatePerson(int ID, Person personToUpdate);

        /// <summary>delete a person by ID</summary>
        /// <param ID="personToDelete">the ID of the peron to delete</param>
        /// <returns>the deleted person</returns>
        Person DeletePerson(int ID);
    }
}
