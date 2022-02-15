using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Tolitech.CodeGenerator.Domain.Queries;
using Tolitech.CodeGenerator.Domain.Tests.Domain.Repositories;

namespace Tolitech.CodeGenerator.Domain.Tests.Domain.Queries.Person.GetAll
{
    public class GetAllQueryHandler : QueryHandler
    {
		private readonly IPersonRepository _personRepository;

		public GetAllQueryHandler(IPersonRepository personRepository, ILogger logger) : base(logger)
		{
			_personRepository = personRepository;
		}

		public IEnumerable<GetAllQueryResult> Handle(GetAllQuery query)
		{
			var result = new List<GetAllQueryResult>();

			query.Validate();
			
			// var item = _personRepository.GetById(query.personId);
			var items = _personRepository.Get();

			foreach (var item in items)
            {
				result.Add(new GetAllQueryResult
				{
					Name = item.Name
				});
            }

			return result;
		}
	}
}
