using System;
using FluentValidation.Results;
using Tolitech.CodeGenerator.Notification;

namespace Tolitech.CodeGenerator.Domain.Entities
{
    public abstract class Entity : Notifiable, IEntity
    {
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