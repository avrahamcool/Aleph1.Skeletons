using Aleph1.Skeletons.WebAPI.WebAPI.Models;
using FluentValidation;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Validators
{
    /// <summary>Validate the LoginModel input</summary>
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        /// <summary>Initializes a new instance of the <see cref="LoginValidator"/> class.</summary>
        public LoginValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(10);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(10);
        }
    }
}