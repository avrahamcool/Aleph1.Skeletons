using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.BL.Contracts;
using Aleph1.Skeletons.WebAPI.Models;
using Aleph1.Skeletons.WebAPI.WebAPI.Security;
using Aleph1.WebAPI.ExceptionHandler;
using System;
using System.Linq;
using System.Web.Http;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Controllers
{
    /// <summary>Handle Person actions</summary>
    public class PersonController : ApiController
    {
        private readonly IBL BL;


        /// <summary>Initializes a new instance of the <see cref="PersonController"/> class.</summary>
        /// <param name="BL">The BL</param>
        [Logged(LogParameters = false)]
        public PersonController(IBL BL)
        {
            this.BL = BL;
        }

        /// <summary>get a list of all persons</summary>
        /// <returns>all persons</returns>
        [Logged, HttpGet, Route("api/Person"), FriendlyMessage("התרחשה שגיאה בשליפת האנשים")]
        public IQueryable<Person> GetPersons()
        {
            return BL.GetPersons();
        }

        /// <summary>get person by ID</summary>
        /// <param name="ID">the ID of the person</param>
        /// <returns>the person</returns>
        [Authenticated, Logged, HttpGet, Route("api/Person/{ID}"), FriendlyMessage("התרחשה שגיאה בשליפת האדם המבוקש")]
        public Person GetPersonByID(int ID)
        {
            return BL.GetPersonByID(ID) ?? throw new Exception("Person Not Found");
        }

        /// <summary>get person by name</summary>
        /// <param name="firstName">the name of the person</param>
        /// <returns>the person</returns>
        [Authenticated, Logged, HttpGet, Route("api/Person/GetPersonByName"), FriendlyMessage("התרחשה שגיאה בשליפת האדם המבוקש")]
        public Person GetPersonByName(string firstName)
        {
            return BL.GetPersonByName(firstName) ?? throw new Exception("Person Not Found");
        }

        /// <summary>Insert a new person</summary>
        /// <param name="person">the person</param>
        /// <returns>the person</returns>
        [Authenticated(RequireManagerAccess = true), Logged, HttpPost, Route("api/Person"), FriendlyMessage("התרחשה שגיאה ביצירת האדם המבוקש")]
        public Person Post(Person person)
        {
            return BL.InsertPerson(person);
        }

        /// <summary>update a given person</summary>
        /// <param name="ID">the ID of the person</param>
        /// <param name="person">the person</param>
        /// <returns>the person</returns>
        [Authenticated(RequireManagerAccess = true), Logged, HttpPut, Route("api/Person/{ID}"), FriendlyMessage("התרחשה שגיאה בעדכון פרטי האדם המבוקש")]
        public Person PutPerson(int ID, [FromBody]Person person)
        {
            Person p = BL.GetPersonByID(ID) ?? throw new Exception("Person Not Found");

            p.FirstName = person.FirstName;
            p.LastName = person.LastName;

            return p;
        }
    }
}