using System;
using System.Collections.Generic;

namespace Tolitech.CodeGenerator.Domain.Models.Notification
{
    public class ClientNotificationResult
    {
        public bool IsValid { get; set; }

        public object Data { get; set; }

        public IList<ClientNotificationMessage> Messages { get; set; }

        public IList<ClientNotificationMessage> Errors { get; set; }

        public class ClientNotificationMessage
        {
            public string Key { get; set; }

            public string Message { get; set; }

            public string Type { get; set; }
        }
    }
}
