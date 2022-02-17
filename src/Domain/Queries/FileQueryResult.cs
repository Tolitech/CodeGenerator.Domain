using System;

namespace Tolitech.CodeGenerator.Domain.Queries
{
    public class FileQueryResult : QueryResult
    {
        public string? Name { get; set; }

        public string? FileName { get; set; }

        public long Length { get; set; }

        public string? ContentType { get; set; }

        public byte[]? File { get; set; }
    }
}
