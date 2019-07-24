namespace PostMortem.Infrastructure.Events.Questions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Domain;
    using Domain.Events.Questions;
    using MediatR;
    using Polly;
    using Zatoichi.Common.Infrastructure.Resilience;

    //public class QuestionAddedHandler : IRequestHandler<AddQuestionCommandArgs, PolicyResult<Guid>>
    //{
    //    private readonly IExecutionPolicies executionPolicies;
    //    private readonly IRepository repository;

    //    public QuestionAddedHandler(
    //        IRepository repository,
    //        IExecutionPolicies executionPolicies)
    //    {
    //        this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
    //        this.repository = Guard.IsNotNull(repository, nameof(repository));
    //    }

    //    public Task<PolicyResult<Guid>> Handle(AddQuestionCommandArgs request, CancellationToken cancellationToken)
    //    {
    //        return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.AddQuestionAsync(request.Question));
    //    }
    //}
}