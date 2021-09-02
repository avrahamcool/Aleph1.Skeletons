using Aleph1.Skeletons.WebAPI.Models.Entities;

using FluentValidation;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Validators
{
	/// <summary>Validate Person model</summary>
	public class PersonValidator : AbstractValidator<Person>
	{
		/// <summary>Validation rules</summary>
		public PersonValidator()
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
