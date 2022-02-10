using System;
using FluentValidation;

namespace Tolitech.CodeGenerator.Domain.Tests.Domain.Commands.Person.Insert
{
    public class InsertCommandValidator : AbstractValidator<InsertCommand>
    {
        public InsertCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
