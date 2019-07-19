namespace PostMortem.Domain.EventSourcing.Commands
{
    public abstract class UpdateCommandArgsBase<T> : ICommandArgs
    {
        public T NewValue { get; set; }
        public T OldValue { get; set; }
    }
}