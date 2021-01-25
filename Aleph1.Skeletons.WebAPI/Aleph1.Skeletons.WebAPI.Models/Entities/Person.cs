using System;

namespace Aleph1.Skeletons.WebAPI.Models.Entities
{
	/// <summary>person details</summary>
	public class Person : IEntity
	{
		/// <summary>auto increment (1, 1)</summary>
		public int ID { get; set; }

		/// <summary>first name</summary>
		/// <remarks>256 max length</remarks>
		public string FirstName { get; set; }

		/// <summary>last name</summary>
		/// <remarks>256 max length</remarks>
		public string LastName { get; set; }

		/// <summary>birth date</summary>
		public DateTimeOffset BirthDate { get; set; }
	}
}
