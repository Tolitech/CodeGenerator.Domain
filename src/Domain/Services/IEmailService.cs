using System;
using System.Net.Mail;

namespace Tolitech.CodeGenerator.Domain.Services
{
    public interface IEmailService : IInfrastructureService
    {
        string GetTemplate(string filePath, params string?[] parameters);

        void Send(string host, int port, string username, string password, string from, string to, string subject, string body, string? cc = null, string? bcc = null, IList<Attachment>? attachments = null);
    }
}
