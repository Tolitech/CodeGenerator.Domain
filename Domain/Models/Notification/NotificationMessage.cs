using System;

namespace Tolitech.CodeGenerator.Domain.Models.Notification
{
    public class NotificationMessage
    {
        public string Key { get; set; }

        public string Message { get; set; }

        public string Type { get; set; }

        public NotificationMessage(string message)
        {
            Message = message;
            Type = "info";
        }

        public NotificationMessage(string message, string type)
        {
            Message = message;
            Type = type;
        }

        public NotificationMessage(string key, string message, string type)
        {
            Key = key;
            Message = message;
            Type = type;
        }
    }
}
