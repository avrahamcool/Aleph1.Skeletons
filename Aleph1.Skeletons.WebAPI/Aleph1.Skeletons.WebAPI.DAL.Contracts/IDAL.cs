using Aleph1.Skeletons.WebAPI.Models;

using System;
using System.Linq;

namespace Aleph1.Skeletons.WebAPI.DAL.Contracts
{
    /// <summary>Handles data access</summary>
    public interface IDAL : IDisposable
    {
        /// <summary>apply all the changes on to the DB</summary>
        void SaveChanges();


        /// <summary>get a list of all persons</summary>
        /// <returns>all persons</returns>
        IQueryable<Person> GetPersons();

        /// <summary>get person by ID</summary>
        /// <param name="ID">the ID of the person</param>
        /// <returns>the person, or null if not found</returns>
        Person GetPersonByID(int ID);

        /// <summary>Insert a new person</summary>
        /// <param name="person">the person</param>
        /// <returns>the person that was added (ID should be available after SaveChanges)</returns>
        Person InsertPerson(Person person);

        /// <summary>Delete a person given its ID</summary>
        /// <param name="ID">the ID of the person</param>
        /// <returns>the person that was deleted</returns>
        Person DeletePerson(int ID);
    }
}
