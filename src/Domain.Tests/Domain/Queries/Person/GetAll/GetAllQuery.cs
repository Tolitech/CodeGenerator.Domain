using System;
using Tolitech.CodeGenerator.Domain.Queries;

namespace Tolitech.CodeGenerator.Domain.Tests.Domain.Queries.Person.GetAll
{
    public class GetAllQuery : Query
    {
        public string? Param1 { get; set; }

        public override void Validate()
        {
            var validator = new GetAllQueryValidator();
            Validate(validator.Validate(this));
        }
    }
}
