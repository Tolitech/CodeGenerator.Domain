using System;
using Tolitech.CodeGenerator.Domain.Entities;

namespace Tolitech.CodeGenerator.Domain.Tests.Domain.Entities
{
    public class Person : Entity
    {
        public Person(string? name)
        {
            Name = name;
        }

        public string? Name { get; private set; }
    }
}