namespace PostMortem.Domain.Events
{
    public abstract class UpdateExpressionCommand<T, TK> : ExpressionEventBase<T>
    {
        public virtual TK NewValue { get; set; }
        public virtual TK OldValue { get; set; }
    }
}