using Aleph1.Skeletons.WebAPI.Models.Security;

using FluentValidation;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Validators
{
	/// <summary>Validate user credentials</summary>
	public class CredentialsValidator : AbstractValidator<Credentials>
	{
		/// <summary>Validation rules</summary>
		public CredentialsValidator()
		{
			RuleFor(x => x.Username).NotEmpty();
			RuleFor(x => x.Password).NotEmpty();
		}
	}
}
