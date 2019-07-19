using System;
using System.Collections.Generic;
using System.Text;

namespace PostMortem.Domain.EventSourcing.Events
{
    public abstract class EventArgsBase: EventArgs, IEventArgs
    {
        public bool IsReplaying { get; set; }
        public abstract IEntityId GetEntityId();
    }
}
