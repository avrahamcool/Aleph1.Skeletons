using Aleph1.Skeletons.Proxy.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aleph1.Skeletons.Proxy.Proxy.Contracts
{
    /// <summary>
    /// Handles data access
    /// </summary>
    public interface IProxy
    {
        /// <summary>get a list of all persons</summary>
        /// <returns>all persons</returns>
        Task<List<Person>> GetPersons();

        /// <summary>Insert a new person</summary>
        /// <param name="person">the person</param>
        /// <returns>the person</returns>
        Task<Person> InsertPerson(Person person);
    }
}
