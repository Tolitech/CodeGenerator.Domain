using System;
using FluentValidation.Results;
using Tolitech.CodeGenerator.Notification;

namespace Tolitech.CodeGenerator.Domain.Queries
{
    public abstract class Query : Notifiable, IQuery
    {
        private Guid? _loggedUserId;

        public Guid? LoggedUserId { get { return _loggedUserId; } }

        public bool HasLoggedUser { get { return LoggedUserId.HasValue; } }

        public void SetLoggedUser(Guid? loggedUserId)
        {
            _loggedUserId = loggedUserId;
        }

        public void Validate(ValidationResult result)
        {
            NotificationResult.Clear();

            foreach (var error in result.Errors)
            {
                NotificationResult.AddError(error.ErrorMessage);
            }
        }
    }
}
