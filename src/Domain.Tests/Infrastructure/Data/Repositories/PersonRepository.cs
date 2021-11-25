using System;
using System.Collections.Generic;
using System.Linq;
using Tolitech.CodeGenerator.Domain.Tests.Domain.Entities;
using Tolitech.CodeGenerator.Domain.Tests.Domain.Repositories;
using Tolitech.CodeGenerator.Notification;

namespace Tolitech.CodeGenerator.Domain.Tests.Infrastructure.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IList<Person> people;

        public PersonRepository()
        {
            people = new List<Person>();

            for (int i = 0; i < 10; i++)
            {
                people.Add(new Person($"Person {i + 1}"));
            }
        }

        public NotificationResult Insert(Person entity)
        {
            var result = new NotificationResult();
            
            people.Add(entity);
            result.AddMessage("Inserted");
            
            return result;
        }

        public IEnumerable<Person> Get()
        {
            return people.ToList();
        }
    }
}
