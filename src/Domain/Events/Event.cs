using System;

namespace Tolitech.CodeGenerator.Domain.Events
{
    public abstract class Event : IEvent
    {
        public Guid? LoggedUserId { get; set; }
    }
}
