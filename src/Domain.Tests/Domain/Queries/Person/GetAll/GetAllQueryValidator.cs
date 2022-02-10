using System;
using FluentValidation;

namespace Tolitech.CodeGenerator.Domain.Tests.Domain.Queries.Person.GetAll
{
    public class GetAllQueryValidator : AbstractValidator<GetAllQuery>
    {
        public GetAllQueryValidator()
        {
            RuleFor(x => x.Param1)
                .NotEmpty();
        }
    }
}
