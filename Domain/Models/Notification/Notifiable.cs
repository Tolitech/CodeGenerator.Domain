using System;

namespace Tolitech.CodeGenerator.Domain.Models.Notification
{
    public abstract class Notifiable
    {
        protected internal NotificationResult NotificationResult { get; set; }

        protected Notifiable()
        {
            NotificationResult = new NotificationResult();
        }

        public bool IsValid()
        {
            Validate();
            return NotificationResult.IsValid;
        }

        public NotificationResult GetNotifications()
        {
            return NotificationResult;
        }

        public virtual void Validate()
        {

        }
    }
}