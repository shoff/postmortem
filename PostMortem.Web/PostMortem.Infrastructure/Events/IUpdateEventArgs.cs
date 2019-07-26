namespace PostMortem.Infrastructure.Events
{
    public interface IUpdateEventArgs<out TProp> : IEventArgs
    {
        TProp NewValue { get; }
        TProp OldValue { get; }
    }
}
