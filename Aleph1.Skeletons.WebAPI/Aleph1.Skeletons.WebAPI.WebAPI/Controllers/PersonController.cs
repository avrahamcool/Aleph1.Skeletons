using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.BL.Contracts;
using Aleph1.Skeletons.WebAPI.Models;
using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.WebAPI.Security;
using Aleph1.WebAPI.ExceptionHandler;

using System.Linq;
using System.Web.Http;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Controllers
{
    /// <summary>Handle Person actions</summary>
    public class PersonController : ApiController
    {
        private readonly IBL BL;

        /// <summary></summary>
        public PersonController(IBL BL)
        {
            this.BL = BL;
        }

        /// <summary></summary>
        protected override void Dispose(bool disposing)
        {
            BL.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>get a list of all persons</summary>
        /// <returns>all persons</returns>
        [Authenticated, Logged, HttpGet, Route("api/Person"), FriendlyMessage("התרחשה שגיאה בשליפת האנשים")]
        public IQueryable<Person> GetPersons()
        {
            return BL.GetPersons();
        }

        /// <summary>get the number of persons currently saved in our system</summary>
        /// <returns>the number of persons in our system</returns>
        [Authenticated(Roles.Anonymous), Logged, HttpGet, Route("api/Person/Count"), FriendlyMessage("התרחשה שגיאה בשליפת כמות האנשים")]
        public int GetPersonsCount()
        {
            return BL.GetPersonsCount();
        }

        /// <summary>get person by ID</summary>
        /// <param name="ID">the ID of the person</param>
        /// <returns>the person</returns>
        [Authenticated, Logged, HttpGet, Route("api/Person/{ID}"), FriendlyMessage("התרחשה שגיאה בשליפת האדם המבוקש")]
        public Person GetPersonByID(int ID)
        {
            return BL.GetPersonByID(ID);
        }

        /// <summary>get a list of persons with a name including the search term</summary>
        /// <param name="searchTerm">a string to query against the persons full name</param>
        /// <returns>the persons that include the given query in their name</returns>
        [Authenticated, Logged, HttpGet, Route("api/Person/SearchByName"), FriendlyMessage("התרחשה שגיאה בחיפוש")]
        public IQueryable<Person> GetPersonByName(string searchTerm)
        {
            return BL.SearchByName(searchTerm);
        }

        /// <summary>Insert a new person</summary>
        /// <param name="person">the person</param>
        /// <returns>the person</returns>
        [Authenticated, Logged, HttpPost, Route("api/Person"), FriendlyMessage("התרחשה שגיאה ביצירת האדם המבוקש")]
        public Person Post(Person person)
        {
            return BL.InsertPerson(person);
        }

        /// <summary>update a given person</summary>
        /// <param name="ID">the ID of the person</param>
        /// <param name="person">the person</param>
        /// <returns>the person</returns>
        [Authenticated, Logged, HttpPut, Route("api/Person/{ID}"), FriendlyMessage("התרחשה שגיאה בעדכון פרטי האדם המבוקש")]
        public Person PutPerson(int ID, [FromBody] Person person)
        {
            return BL.UpdatePerson(ID, person);
        }

        /// <summary>delete a person by ID</summary>
        /// <param ID="personToDelete">the ID of the person to delete</param>
        /// <returns>the deleted person</returns>
        [Authenticated(Roles.Admin), Logged, HttpDelete, Route("api/Person/{ID}"), FriendlyMessage("התרחשה שגיאה במחיקת האדם המבוקש")]
        public Person DeletePerson(int ID)
        {
            return BL.DeletePerson(ID);
        }
    }
}