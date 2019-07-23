using System;
using System.Collections.Generic;
using System.Text;

namespace PostMortem.Domain.EventSourcing.Events
{
    public interface IUpdateEventArgs<out TProp> : IEventArgs
    {
        TProp NewValue { get; }
        TProp OldValue { get; }
    }
}
