using System;
using FluentValidation;

namespace Tolitech.CodeGenerator.Domain.Queries
{
    public class PageQueryValidator : AbstractValidator<PageQuery>
    {
        public PageQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);

            RuleFor(x => x.OrderBy)
                .NotEmpty();
        }
    }
}