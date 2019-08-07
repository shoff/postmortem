namespace PostMortem.Infrastructure.Questions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Commands;
    using Domain;
    using MediatR;
    using Zatoichi.Common.Infrastructure.Resilience;

    public class QuestionAddedHandler : INotificationHandler<AddQuestionCommand>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly IRepository repository;

        public QuestionAddedHandler(
            IRepository repository,
            IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public Task Handle(AddQuestionCommand notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}