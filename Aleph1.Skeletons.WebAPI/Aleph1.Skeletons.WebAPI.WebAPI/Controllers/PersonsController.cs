using System.Collections.Generic;
using System.Web.Http;

using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.BL.Contracts;
using Aleph1.Skeletons.WebAPI.Models.Entities;
using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.WebAPI.Security;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Controllers
{
	/// <summary>Persons controller</summary>
	public class PersonsController : ApiController
	{
		private readonly IPersonBL BL;

		/// <summary>Constructor</summary>
		public PersonsController(IPersonBL BL) => this.BL = BL;

		/// <summary>Dispose</summary>
		protected override void Dispose(bool disposing)
		{
			BL.Dispose();
			base.Dispose(disposing);
		}

		/// <summary>Get a list of all persons</summary>
		/// <returns>List of persons</returns>
		[Authenticated, Logged, HttpGet, Route("api/persons")]
		public IEnumerable<Person> GetPersons() => BL.GetPersons();

		/// <summary>Get the number of persons currently saved in the system</summary>
		/// <returns>Number of persons</returns>
		[Authenticated(Roles.None), Logged, HttpGet, Route("api/persons/count")]
		public int GetPersonsCount() => BL.GetPersonsCount();

		/// <summary>Gets a single person by identification number</summary>
		/// <param name="Id">Unique identification number</param>
		/// <returns>A single person</returns>
		[Authenticated, Logged, HttpGet, Route("api/persons/{Id}")]
		public Person GetPersonById(int Id) => BL.GetPersonById(Id);

		/// <summary>Find persons whose name includes the search query</summary>
		/// <param name="name">Search query string</param>
		/// <returns>List of persons</returns>
		[Authenticated(Roles.Admin), Logged, HttpGet, Route("api/persons/search")]
		public IEnumerable<Person> SearchByName(string name) => BL.SearchByName(name);

		/// <summary>Insert a new person</summary>
		/// <param name="person">Person to be inserted</param>
		/// <returns>Inserted person</returns>
		[Authenticated, Logged, HttpPost, Route("api/persons")]
		public Person InsertPerson(Person person) => BL.InsertPerson(person);

		/// <summary>Update a person's details</summary>
		/// <param name="Id">Selected person identification number</param>
		/// <param name="person">Person to be updated</param>
		/// <returns>Updated person</returns>
		[Authenticated, Logged, HttpPut, Route("api/persons/{Id}")]
		public Person UpdatePerson(int Id, [FromBody] Person person) => BL.UpdatePerson(Id, person);

		/// <summary>Delete a person by their identification number</summary>
		/// <param name="Id">Selected person identification number</param>
		/// <returns>Deleted person</returns>
		[Authenticated, Logged, HttpDelete, Route("api/persons/{Id}")]
		public Person DeletePerson(int Id) => BL.DeletePerson(Id);
	}
}
