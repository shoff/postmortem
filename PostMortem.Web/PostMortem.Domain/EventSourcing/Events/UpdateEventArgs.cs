using System;
using System.Collections.Generic;
using System.Text;

namespace PostMortem.Domain.EventSourcing.Events
{
    public abstract class UpdateEventArgsBase<T> : EventArgsBase
    {
        public T NewValue { get; set; }
        public T OldValue { get; set; }
    }
}
