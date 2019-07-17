namespace PostMortem.Domain.Events.Questions
{
    using System;
    using System.Linq.Expressions;
    using ChaosMonkey.Guards;
    using Domain.Questions;
    using MediatR;
    using Newtonsoft.Json;
    using Polly;

    public class QuestionImportanceEventArgs : ExpressionEventBase<Question>, IRequest<PolicyResult>
    {

        // To make serializer happy
        public QuestionImportanceEventArgs() { }

        public QuestionImportanceEventArgs(Guid questionId, int importance, int change)
        {
            QuestionId = questionId;
            this.Importance = importance;
            this.Change = change;
            Expression<Func<int>> apply = () => this.Importance + this.Change;
            this.Expression = JsonConvert.SerializeObject(apply);
        }

        public override Question Apply(Question t)
        {
            Guard.IsNotNull(t, nameof(t));
            Expression<Func<int>> exp = JsonConvert.DeserializeObject<Expression<Func<int>>>(this.Expression);
            t.Importance = exp.Compile().Invoke();
            return t;
        }
        public sealed override string Expression { get; protected set; }
        public Guid QuestionId { get; set; }
        public int Importance { get; set; }
        public int Change { get; set; }

    }
}