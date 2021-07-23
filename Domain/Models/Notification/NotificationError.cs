using System;

namespace Tolitech.CodeGenerator.Domain.Models.Notification
{
    public class NotificationError : NotificationMessage
    {
        protected Exception Exception { get; set; }

        public NotificationError(string message) : base(message, "error") { }

        public NotificationError(Exception ex) : base(ex.Message, "error")
        {
            Exception = ex;
        }

        public NotificationError(string key, string message) : base(key, message, "error") { }

        public NotificationError(string key, Exception ex) : base(key, ex.Message, "error")
        {
            Exception = ex;
        }

        public Exception GetException()
        {
            return Exception;
        }
    }
}
