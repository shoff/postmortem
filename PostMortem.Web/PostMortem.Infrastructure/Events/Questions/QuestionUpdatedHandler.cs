﻿namespace PostMortem.Infrastructure.Events.Questions
{
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Domain;
    using Domain.Questions.Events;
    using MediatR;
    using Polly;
    using Zatoichi.Common.Infrastructure.Resilience;

    public class QuestionUpdatedHandler : IRequestHandler<QuestionUpdatedEventArgs, PolicyResult>
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

        public Task<PolicyResult> Handle(QuestionUpdatedEventArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.UpdateQuestionAsync(request.Question));
        }
    }
}