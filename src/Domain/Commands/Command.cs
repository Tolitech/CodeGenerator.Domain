using System;
using FluentValidation.Results;
using Tolitech.CodeGenerator.Notification;

namespace Tolitech.CodeGenerator.Domain.Commands
{
    public abstract class Command : Notifiable, ICommand
    {
        private string? _loggedUserId;

        public string? LoggedUserId { get { return _loggedUserId; } }

        public bool HasLoggedUser { get { return !string.IsNullOrEmpty(LoggedUserId); } }

        public void SetLoggedUser(string? loggedUserId)
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
