using System;

namespace Tolitech.CodeGenerator.Domain.Queries
{
    public abstract class PageQuery : Query
    {
        public PageQuery()
        {
            PageNumber = 1;
            PageSize = 100;
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string? OrderBy { get; set; }

        public int SkipNumber { get { return (PageNumber - 1) * PageSize; } }

        public override void Validate()
        {
            var validator = new PageQueryValidator();
            Validate(validator.Validate(this));
        }
    }
}