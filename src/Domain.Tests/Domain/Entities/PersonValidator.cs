using System;
using FluentValidation;

namespace Tolitech.CodeGenerator.Domain.Tests.Domain.Entities
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
