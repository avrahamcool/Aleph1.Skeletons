using Aleph1.Skeletons.WebAPI.WebAPI.Models;

using FluentValidation;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Validators
{
    /// <summary>Validate the LoginModel input</summary>
    public class LoginValidatonCollection : AbstractValidator<LoginModel>
    {
        /// <summary>Initializes a new instance of the <see cref="LoginValidatonCollection"/> class.</summary>
        public LoginValidatonCollection()
        {
            //TODO: put your real logic here

            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(30);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(2);
        }
    }
}