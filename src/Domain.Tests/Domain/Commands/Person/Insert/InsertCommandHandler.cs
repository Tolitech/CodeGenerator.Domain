using System;
using Microsoft.Extensions.Logging;
using Tolitech.CodeGenerator.Domain.Commands;
using Tolitech.CodeGenerator.Domain.Services;
using Tolitech.CodeGenerator.Domain.Tests.Domain.Repositories;
using Tolitech.CodeGenerator.Notification;

namespace Tolitech.CodeGenerator.Domain.Tests.Domain.Commands.Person.Insert
{
    public class InsertCommandHandler : CommandHandler
    {
		private readonly IPersonRepository _personRepository;

		public InsertCommandHandler(IUnitOfWorkService uow, IPersonRepository personRepository, ILogger logger) : base(uow, logger)
		{
			_personRepository = personRepository;
		}

		public NotificationResult Handle(InsertCommand command)
        {
			var person = new Entities.Person(command.Name);
			var result = _personRepository.Insert(person);
			return result;
		}
	}
}