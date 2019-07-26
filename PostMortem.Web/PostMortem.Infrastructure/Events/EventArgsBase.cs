using System;
using System.Runtime.Serialization;

namespace PostMortem.Infrastructure.Events
{
    public abstract class EventArgsBase: EventArgs, IEventArgs
    {
        //[IgnoreDataMember]
        //public bool IsReplaying { get; set; }
        public abstract IEntityId GetEntityId();
    }
}
