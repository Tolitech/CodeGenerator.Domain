using System;
using Tolitech.CodeGenerator.Domain.Commands;

namespace Tolitech.CodeGenerator.Domain.Tests.Domain.Commands.Person.Insert
{
    public class InsertCommand : Command
    {
        public string? Name { get; set; }

        public override void Validate()
        {
            var validator = new InsertCommandValidator();
            Validate(validator.Validate(this));
        }
    }
}
