﻿using System.Collections.Generic;
using System.Web.Http;

using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.BL.Contracts;
using Aleph1.Skeletons.WebAPI.Models.Entities;
using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.WebAPI.Security;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Controllers
{
	/// <summary>Handle Person actions</summary>
	public class PersonsController : ApiController
	{
		private readonly IPersonBL BL;

		/// <summary></summary>
		public PersonsController(IPersonBL BL) => this.BL = BL;

		/// <summary></summary>
		protected override void Dispose(bool disposing)
		{
			BL.Dispose();
			base.Dispose(disposing);
		}

		/// <summary>get a list of all persons</summary>
		/// <returns>all persons</returns>
		[Authenticated, Logged, HttpGet, Route("api/persons")]
		public IEnumerable<Person> GetPersons() => BL.GetPersons();

		/// <summary>get the number of persons currently saved in our system</summary>
		/// <returns>the number of persons in our system</returns>
		[Authenticated(Roles.None), Logged, HttpGet, Route("api/persons/count")]
		public int GetPersonsCount() => BL.GetPersonsCount();

		/// <summary>get person by ID</summary>
		/// <param name="ID">the ID of the person</param>
		/// <returns>the person</returns>
		[Authenticated, Logged, HttpGet, Route("api/persons/{ID}")]
		public Person GetPersonByID(int ID) => BL.GetPersonByID(ID);

		/// <summary>get a list of persons with a name including the search term</summary>
		/// <param name="searchTerm">a string to query against the persons full name</param>
		/// <returns>the persons that include the given query in their name</returns>
		[Authenticated(Roles.Admin), Logged, HttpGet, Route("api/persons/search-by-name")]
		public IEnumerable<Person> SearchByName(string searchTerm) => BL.SearchByName(searchTerm);

		/// <summary>Insert a new person</summary>
		/// <param name="person">the person</param>
		/// <returns>the person</returns>
		[Authenticated, Logged, HttpPost, Route("api/persons")]
		public Person Post(Person person) => BL.InsertPerson(person);

		/// <summary>update a given person</summary>
		/// <param name="ID">the ID of the person</param>
		/// <param name="person">the person</param>
		/// <returns>the person</returns>
		[Authenticated, Logged, HttpPut, Route("api/persons/{ID}")]
		public Person PutPerson(int ID, [FromBody] Person person) => BL.UpdatePerson(ID, person);

		/// <summary>delete a person by ID</summary>
		/// <param ID="personToDelete">the ID of the person to delete</param>
		/// <returns>the deleted person</returns>
		[Authenticated, Logged, HttpDelete, Route("api/persons/{ID}")]
		public Person DeletePerson(int ID) => BL.DeletePerson(ID);
	}
}
