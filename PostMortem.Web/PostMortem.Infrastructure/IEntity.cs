namespace PostMortem.Infrastructure
{
    public interface IEntity<out TEntityId>
        where TEntityId : IEntityId
    {
        TEntityId GetEntityId();
    }
}
