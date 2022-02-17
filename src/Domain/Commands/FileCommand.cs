using System;

namespace Tolitech.CodeGenerator.Domain.Commands
{
    public class FileCommand : Command
    {
        public string? Name { get; set; }

        public string? FileName { get; set; }

        public long Length { get; set; }

        public string? ContentType { get; set; }

        public byte[]? File { get; set; }

        public bool HasFile { get { return File != null; } }

        public bool Changed { get; set; }
    }
}
