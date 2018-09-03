using Aleph1.Skeletons.WebAPI.Models;
using System.Linq;

namespace Aleph1.Skeletons.WebAPI.DAL.Contracts
{
    /// <summary>
    /// Handles data access
    /// </summary>
    public interface IDAL
    {
        /// <summary>get a list of all persons</summary>
        /// <returns>all persons</returns>
        IQueryable<Person> GetPersons();

        /// <summary>get person by ID</summary>
        /// <param name="ID">the ID of the person</param>
        /// <returns>the person</returns>
        Person GetPersonByID(int ID);

        /// <summary>Insert a new person</summary>
        /// <param name="person">the person</param>
        /// <returns>the person</returns>
        Person InsertPerson(Person person);
    }
}
