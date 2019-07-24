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

    //public class QuestionResponseCountChangedHandler : IRequestHandler<QuestionResponseCountChangedEventArgs, PolicyResult>
    //{
    //    private readonly IExecutionPolicies executionPolicies;
    //    private readonly IRepository repository;
    //    private readonly EventBroker eventBroker;

    //    public QuestionResponseCountChangedHandler(
    //        IRepository repository,
    //        IExecutionPolicies executionPolicies,
    //        EventBroker eventBroker)
    //    {
    //        this.eventBroker = Guard.IsNotNull(eventBroker, nameof(eventBroker));
    //        this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
    //        this.repository = Guard.IsNotNull(repository, nameof(repository));
    //    }

    //    public Task<PolicyResult> Handle(QuestionResponseCountChangedEventArgs request, CancellationToken cancellationToken)
    //    {
    //        this.eventBroker.ChangeResponseCount(this, request);

    //        return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() =>
    //            this.repository.UpdateQuestionResponseCount(request.QuestionId, request.NewValue));
    //    }
    //}
}