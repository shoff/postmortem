namespace PostMortem.Domain.Events.Questions
{
    using System;
    using System.Linq.Expressions;
    using ChaosMonkey.Guards;
    using Domain.Questions;
    using MediatR;
    using Newtonsoft.Json;
    using Polly;

    public class QuestionResponseCountChangedEventArgs : ExpressionEventBase<Question>, IRequest<PolicyResult>
    {
        public QuestionResponseCountChangedEventArgs() { }

        public QuestionResponseCountChangedEventArgs(Guid questionId, int oldValue, int newValue)
        {
            this.QuestionId = questionId;
            this.OldValue = oldValue;
            this.NewValue = newValue;
            Expression<Func<int>> apply = () => this.NewValue;
            this.Expression = JsonConvert.SerializeObject(apply);
        }

        public override Question Apply(Question question)
        {
            Guard.IsNotNull(question, nameof(question));
            Expression<Func<int>> exp = JsonConvert.DeserializeObject<Expression<Func<int>>>(this.Expression);
            question.ResponseCount = exp.Compile().Invoke();
            return question;
        }
        
        public sealed override string Expression { get; protected set; }
        public Guid QuestionId { get; set; }
        public int OldValue { get; set; }
        public int NewValue { get; set; }
    }
}