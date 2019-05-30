using Aleph1.Skeletons.Proxy.Models;
using FluentValidation;

namespace Aleph1.Skeletons.Proxy.WebAPI.Validators
{
    /// <summary></summary>
    public class PersonValidator : AbstractValidator<Person>
    {
        /// <summary>Initializes a new instance of the <see cref="PersonValidator"/> class.</summary>
        public PersonValidator()
        {
            RuleFor(x => x.ID).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
        }
    }
}