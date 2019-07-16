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

        public QuestionResponseCountChangedEventArgs(Guid questionId, int responseCount, int change)
        {
            this.QuestionId = questionId;
            this.ResponseCount = responseCount;
            this.Change = change;
            Expression<Func<int>> apply = () => this.ResponseCount + this.Change;
            this.Expression = JsonConvert.SerializeObject(apply);
        }

        public override Question Apply(Question t)
        {
            Guard.IsNotNull(t, nameof(t));
            Expression<Func<int>> exp = JsonConvert.DeserializeObject<Expression<Func<int>>>(this.Expression);
            t.ResponseCount = exp.Compile().Invoke();
            return t;
        }

        public override Question Undo(Question t)
        {
            throw new System.NotImplementedException();
        }
        
        public sealed override string Expression { get; protected set; }
        public Guid QuestionId { get; set; }
        public int ResponseCount { get; set; }
        public int Change { get; set; }
    }
}