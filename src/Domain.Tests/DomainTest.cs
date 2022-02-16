using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Tolitech.CodeGenerator.Domain.Commands;
using Tolitech.CodeGenerator.Domain.Queries;
using Tolitech.CodeGenerator.Domain.Tests.Domain.Entities;
using Tolitech.CodeGenerator.Domain.Tests.Infrastructure.Data.Repositories;
using Tolitech.CodeGenerator.Domain.Tests.Domain.Services;
using Tolitech.CodeGenerator.Notification;
using Tolitech.CodeGenerator.Domain.Tests.Domain.Events.Person;

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

        [Fact(DisplayName = "Entity - Validate - Invalid")]
        public void Entity_Validate_Invalid()
        {
            var person = new Person(null);
            Assert.False(person.IsValid());
        }

        [Fact(DisplayName = "Command - Validate - Valid")]
        public void Command_Validate_Valid()
        {
            var command = new Domain.Commands.Person.Insert.InsertCommand { Name = "Name" };
            Assert.True(command.IsValid());
        }

        [Fact(DisplayName = "Command - Validate - Invalid")]
        public void Command_Validate_Invalid()
        {
            var command = new Domain.Commands.Person.Insert.InsertCommand { Name = "" };
            Assert.False(command.IsValid());
        }

        [Fact(DisplayName = "Query - Validate - Valid")]
        public void Query_Validate_Valid()
        {
            var query = new Domain.Queries.Person.GetAll.GetAllQuery { Param1 = "Test" };
            Assert.True(query.IsValid());
        }

        [Fact(DisplayName = "Query - Validate - Invalid")]
        public void Query_Validate_Invalid()
        {
            var query = new Domain.Queries.Person.GetAll.GetAllQuery { Param1 = "" };
            Assert.False(query.IsValid());
        }

        [Fact(DisplayName = "CommandHandler - Insert - Valid")]
        public void CommandHandler_Insert_Valid()
        {
            var logger = new Mock<ILogger>();
            var result = new NotificationResult();
            var uow = new UnitOfWorkService();

            var command = new Domain.Commands.Person.Insert.InsertCommand()
            {
                Name = "Person Eleven"
            };
            
            if (command.IsValid())
            {
                var repository = new PersonRepository();
                var handler = new Domain.Commands.Person.Insert.InsertCommandHandler(uow, repository, logger.Object);
                result.Add(handler.Handle(command));
            }

            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "QueryHandler - GetAll - Valid")]
        public void QueryHandler_GetAll_Valid()
        {
            var logger = new Mock<ILogger>();
            var uow = new UnitOfWorkService();
            var command = new Domain.Commands.Person.Insert.InsertCommand()
            {
                Name = "Person Eleven"
            };

            var repository = new PersonRepository();
            var commandHandler = new Domain.Commands.Person.Insert.InsertCommandHandler(uow, repository, logger.Object);
            commandHandler.Handle(command);

            var query = new Domain.Queries.Person.GetAll.GetAllQuery();
            var queryHandler = new Domain.Queries.Person.GetAll.GetAllQueryHandler(repository, logger.Object);
            var result = queryHandler.Handle(query);

            query.SetLoggedUser(Guid.NewGuid());

            Assert.True(result.Count() == 11);
            Assert.True(result.Last().Name == command.Name);
            Assert.True(query.HasLoggedUser);
        }


        [Fact(DisplayName = "FileCommand - HasFile - Valid")]
        public void FileCommand_HasFile_Valid()
        {
            var command = new FileCommand
            {
                Name = "Name",
                FileName = "FileName",
                ContentType = "ContentType",
                File = null,
                Length = 0,
                Changed = false
            };

            command.SetLoggedUser(Guid.NewGuid());

            Assert.False(command.HasFile);
            Assert.True(command.HasLoggedUser);
        }

        [Fact(DisplayName = "FileQueryResult - Instance - Valid")]
        public void FileQuery_Instance_Valid()
        {
            var result = new FileQueryResult
            {
                Name = "Name",
                FileName = "FileName",
                ContentType = "ContentType",
                File = null,
                Length = 0
            };

            Assert.NotNull(result);
        }

        [Fact(DisplayName = "PageQuery - CorrectData - Valid")]
        public void PageQuery_CorrectData_Valid()
        {
            // Arrange
            var query = new Domain.Queries.Page.PageQuery()
            {
                PageNumber = 1,
                PageSize = 10,
                OrderBy = "name"
            };

            // Act
            query.Validate();

            // Assert
            Assert.True(query.IsValid() && query.SkipNumber == 0);
        }

        [Fact(DisplayName = "PageQuery - IncorrectData - Invalid")]
        public void PageQuery_IncorrectData_Invalid()
        {
            // Arrange
            var query = new Domain.Queries.Page.PageQuery()
            {
                PageNumber = 0,
                PageSize = 0
            };

            // Act
            query.Validate();

            // Assert
            Assert.False(query.IsValid());
        }

        [Fact(DisplayName = "PageQueryHandler - LogQuery - Valid")]
        public void PageQueryHandler_LogQuery_Valid()
        {
            // Arrange
            var query = new Domain.Queries.Page.PageQuery()
            {
                PageNumber = 1,
                PageSize = 10,
                OrderBy = "name"
            };

            var logger = new Mock<ILogger>();
            var queryHandler = new Domain.Queries.Page.PageQueryHandler(logger.Object);
            var ex = new Exception("test");

            // Act
            queryHandler.LogQueryHandlerException(ex, query);

            // Assert
            Assert.True(query.IsValid() && query.SkipNumber == 0);
        }

        [Fact(DisplayName = "CommandHandler - LogCommand - Valid")]
        public void CommandHandler_LogCommand_Valid()
        {
            var logger = new Mock<ILogger>();
            var uow = new UnitOfWorkService();
            var result = new NotificationResult();

            var command = new Domain.Commands.Person.Insert.InsertCommand()
            {
                Name = "Person"
            };

            var repository = new PersonRepository();
            var handler = new Domain.Commands.Person.Insert.InsertCommandHandler(uow, repository, logger.Object);
            result.Add(handler.Handle(command));

            var ex = new Exception("test");
            handler.LogCommandHandlerException(ex, command);

            handler.BeginTransaction();
            handler.Rollback(result);
            handler.Commit(result);

            result.AddError("error");
            handler.Commit(result);

            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "CommandHandler - Messages - Valid")]
        public void CommandHandler_Messages_Valid()
        {
            var logger = new Mock<ILogger>();
            var uow = new UnitOfWorkService();

            var repository = new PersonRepository();
            var handler = new Domain.Commands.Person.Insert.InsertCommandHandler(uow, repository, logger.Object);

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            var result1 = new NotificationResult();
            handler.DefaultMessageInsert(result1);

            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            var result2 = new NotificationResult();
            handler.DefaultMessageUpdate(result2);

            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");
            var result3 = new NotificationResult();
            handler.DefaultMessageDelete(result3);

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            var result4 = new NotificationResult();
            handler.DefaultMessageUpload(result4);

            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            var result5 = new NotificationResult();
            result5.AddError("error");
            handler.DefaultMessageInsert(result5);

            Assert.True(result1.Messages.First().Message == "Registry successfully entered.");
            Assert.True(result2.Messages.First().Message == "Registro atualizado com sucesso.");
            Assert.True(result3.Messages.First().Message == "Registro eliminado con éxito.");
            Assert.True(result4.Messages.First().Message == "File updated successfully.");
            Assert.True(result5.Errors.First().Message == "Ocorreu um erro ao inserir o registro.");
        }

        [Fact(DisplayName = "Event - LoggerUserId - Valid")]
        public void Event_LoggedUserId_Valid()
        {
            var e = new PersonEvent() { LoggedUserId = Guid.NewGuid() };
            Assert.True(e.LoggedUserId.HasValue);
        }
    }
}
