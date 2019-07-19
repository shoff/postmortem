using System;
using System.Collections.Generic;
using System.Text;

namespace PostMortem.Domain
{
    public abstract class AggregateId<TId> : IAggregateId
        where TId : struct
    {
        public TId Id { get; }
        protected AggregateId(TId id)
        {
            Id= id;
        }

        public abstract string AsIdString();

        public override bool Equals(object obj)
        {
            return obj is AggregateId<TId> aggregateId && Equals(Id, aggregateId.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
