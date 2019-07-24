using System;
using System.Collections.Generic;
using System.Text;

namespace PostMortem.Domain
{
    public abstract class EntityId<TId> : IEntityId
        where TId : struct
    {
        public TId Id { get; }
        protected EntityId(TId id) { Id= id; }
        public abstract string AsIdString();
        public override bool Equals(object obj)
        {
            return obj is EntityId<TId> entityId && Equals(Id, entityId.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static implicit operator TId(EntityId<TId> id) => id.Id;
        public static implicit operator string(EntityId<TId> id) => id.AsIdString();
    }
}
