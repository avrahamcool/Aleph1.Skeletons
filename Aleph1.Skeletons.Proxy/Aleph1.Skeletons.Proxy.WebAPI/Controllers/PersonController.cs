using Aleph1.Logging;
using Aleph1.Skeletons.Proxy.Models;
using Aleph1.Skeletons.Proxy.Proxy.Contracts;
using Aleph1.WebAPI.ExceptionHandler;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Controllers
{
    /// <summary>Handle Person actions</summary>
    public class PersonController : ApiController, IProxy
    {
        private readonly IProxy proxy;


        /// <summary>Initializes a new instance of the <see cref="PersonController"/> class.</summary>
        /// <param name="proxy">The BL</param>
        public PersonController(IProxy proxy)
        {
            this.proxy = proxy;
        }

        /// <summary>get a list of all persons</summary>
        /// <returns>all persons</returns>
        [Logged, HttpGet, Route("api/Person"), FriendlyMessage("התרחשה שגיאה בשליפת האנשים")]
        public async Task<List<Person>> GetPersons()
        {
            return await proxy.GetPersons();
        }

        /// <summary>Insert a new person</summary>
        /// <param name="person">the person</param>
        /// <returns>the person</returns>
        [Logged, HttpPost, Route("api/Person"), FriendlyMessage("התרחשה שגיאה ביצירת האדם המבוקש")]
        public async Task<Person> InsertPerson(Person person)
        {
            return await proxy.InsertPerson(person);
        }
    }
}