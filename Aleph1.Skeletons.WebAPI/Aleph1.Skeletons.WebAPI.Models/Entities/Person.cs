using System;

namespace Aleph1.Skeletons.WebAPI.Models.Entities
{
	/// <summary>Person entity</summary>
	public class Person : IWritableEntity
	{
		/// <summary>Unique identification number</summary>
		public int Id { get; set; }

		/// <summary>First name</summary>
		public string FirstName { get; set; }

		/// <summary>Last name</summary>
		public string LastName { get; set; }

		/// <summary>Birth date</summary>
		public DateTimeOffset Birthdate { get; set; }
	}
}
