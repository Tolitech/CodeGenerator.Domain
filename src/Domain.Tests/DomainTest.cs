﻿using System;
using System.Linq;
using Xunit;
using Tolitech.CodeGenerator.Domain.Tests.Domain.Entities;
using Tolitech.CodeGenerator.Domain.Tests.Infrastructure.Data.Repositories;
using Tolitech.CodeGenerator.Notification;

namespace Tolitech.CodeGenerator.Domain.Tests
{
    public class DomainTest
    {
        [Fact(DisplayName = "Entity - Validate - Valid")]
        public void Entity_Validate_Valid()
        {
            var person = new Person("Name");
            Assert.True(person.IsValid());
        }

        [Fact(DisplayName = "CommandHandler - Insert - Valid")]
        public void CommandHandler_Insert_Valid()
        {
            var result = new NotificationResult();

            var command = new Domain.Commands.Person.Insert.InsertCommand()
            {
                Name = "Person Eleven"
            };
            
            if (command.IsValid())
            {
                var repository = new PersonRepository();
                var handler = new Domain.Commands.Person.Insert.InsertCommandHandler(repository);
                result.Add(handler.Handle(command));
            }

            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "QueryHandler - GetAll - Valid")]
        public void QueryHandler_GetAll_Valid()
        {
            var command = new Domain.Commands.Person.Insert.InsertCommand()
            {
                Name = "Person Eleven"
            };

            var repository = new PersonRepository();
            var commandHandler = new Domain.Commands.Person.Insert.InsertCommandHandler(repository);
            commandHandler.Handle(command);

            var query = new Domain.Queries.Person.GetAll.GetAllQuery();
            var queryHandler = new Domain.Queries.Person.GetAll.GetAllQueryHandler(repository);
            var result = queryHandler.Handle(query);

            Assert.True(result.Count() == 11);
            Assert.True(result.Last().Name == command.Name);
        }
    }
}