using System;
using Microsoft.Extensions.Logging;

namespace Tolitech.CodeGenerator.Domain.Tests.Domain.Queries.Page
{
    public class PageQueryHandler : CodeGenerator.Domain.Queries.QueryHandler
    {
        public PageQueryHandler(ILogger logger) : base(logger)
        {

        }
    }
}
