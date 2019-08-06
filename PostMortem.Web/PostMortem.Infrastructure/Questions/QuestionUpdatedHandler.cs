namespace PostMortem.Infrastructure.Questions
{
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Domain;
    using Domain.Questions.Commands;
    using MediatR;
    using Zatoichi.Common.Infrastructure.Resilience;

    public class QuestionUpdatedHandler : INotificationHandler<UpdateQuestionCommand>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly IRepository repository;

        public QuestionUpdatedHandler(
            IRepository repository,
            IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        Task INotificationHandler<UpdateQuestionCommand>.Handle(UpdateQuestionCommand notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}