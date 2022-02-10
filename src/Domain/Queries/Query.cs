using System;
using Tolitech.CodeGenerator.Notification;

namespace Tolitech.CodeGenerator.Domain.Queries
{
    public abstract class Query : Notifiable, IQuery
    {
        private string? _loggedUserId;

        public string? LoggedUserId { get { return _loggedUserId; } }

        public bool HasLoggedUser { get { return !string.IsNullOrEmpty(LoggedUserId); } }

        public void SetLoggedUser(string? loggedUserId)
        {
            _loggedUserId = loggedUserId;
        }
    }
}
