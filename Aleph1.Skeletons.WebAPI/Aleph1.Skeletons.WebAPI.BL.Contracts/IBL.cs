using Aleph1.Skeletons.WebAPI.Models;
using System.Linq;

namespace Aleph1.Skeletons.WebAPI.BL.Contracts
{
    /// <summary>
    /// Handles business logic
    /// </summary>
    public interface IBL
    {
        /// <summary>get a list of all persons</summary>
        /// <returns>all persons</returns>
        IQueryable<Person> GetPersons();

        /// <summary>get person by ID</summary>
        /// <param name="ID">the ID of the person</param>
        /// <returns>the person</returns>
        Person GetPersonByID(int ID);

        /// <summary>get person by name</summary>
        /// <param name="firstName">the name of the person</param>
        /// <returns>the person</returns>
        Person GetPersonByName(string firstName);

        /// <summary>Insert a new person</summary>
        /// <param name="person">the person</param>
        /// <returns>the person</returns>
        Person InsertPerson(Person person);
    }
}
