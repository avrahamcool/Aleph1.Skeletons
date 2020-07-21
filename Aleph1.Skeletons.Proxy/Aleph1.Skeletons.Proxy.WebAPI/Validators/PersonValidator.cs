using Aleph1.Skeletons.Proxy.Models;

using FluentValidation;

namespace Aleph1.Skeletons.Proxy.WebAPI.Validators
{
    /// <summary>Validates the Person input</summary>
    public class PersonValidator : AbstractValidator<Person>
    {
        /// <summary>Initializes a new instance of the <see cref="PersonValidator"/> class.</summary>
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