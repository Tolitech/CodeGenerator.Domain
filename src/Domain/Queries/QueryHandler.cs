using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace Tolitech.CodeGenerator.Domain.Queries
{
    public abstract class QueryHandler : IQueryHandler
    {
        protected readonly ILogger _logger;

        public QueryHandler(ILogger logger)
        {
            _logger = logger;
        }

        #region Logger

        public void LogQueryHandlerException<T>(Exception ex, T query) where T : Query
        {
            const string template = "QueryParams: {parameters}";

            string parameters = JsonSerializer.Serialize(query, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });

            _logger.LogWarning(ex, ex.Message + "\n" + template, parameters);
        }

        #endregion
    }
}