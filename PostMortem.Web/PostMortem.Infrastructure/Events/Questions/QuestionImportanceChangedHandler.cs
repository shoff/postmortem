namespace PostMortem.Infrastructure.Events.Questions
{
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Domain;
    using Domain.Events.Questions;
    using MediatR;
    using Polly;
    using Zatoichi.Common.Infrastructure.Resilience;

    public class QuestionImportanceChangedHandler : IRequestHandler<QuestionImportanceEventArgs, PolicyResult>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly IRepository repository;
        private readonly EventBroker eventBroker;

        public QuestionImportanceChangedHandler(
            IRepository repository,
            IExecutionPolicies executionPolicies,
            EventBroker eventBroker)
        {
            this.eventBroker = Guard.IsNotNull(eventBroker, nameof(eventBroker));
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public Task<PolicyResult> Handle(QuestionImportanceEventArgs request, CancellationToken cancellationToken)
        {
            this.eventBroker.ChangeResponseCount(this, request);

            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() =>
                this.repository.UpdateQuestionResponseCount(request.QuestionId, request.Importance + request.Change));
        }
    }
}