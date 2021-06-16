using Aleph1.Skeletons.WebAPI.Models.Entities;

using FluentValidation;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Validators
{
	/// <summary>Validates the Person input</summary>
	public class PersonValidationsCollection : AbstractValidator<Person>
	{
		/// <summary>Initializes a new instance of the <see cref="PersonValidationsCollection"/> class.</summary>
		public PersonValidationsCollection()
		{
			RuleFor(x => x.FirstName)
				.NotEmpty()
				.MinimumLength(2);

			RuleFor(x => x.LastName)
				.NotEmpty()
				.MinimumLength(2);
		}
	}
}
