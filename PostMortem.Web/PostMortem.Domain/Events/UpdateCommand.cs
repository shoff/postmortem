namespace PostMortem.Domain.Events
{
    public abstract class UpdateCommand<T, TK> : Command<T>
    {
        public virtual TK NewValue { get; set; }
        public virtual TK OldValue { get; set; }
    }
}