namespace PostMortem.Infrastructure.Events.Comments
{
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Domain;
    using Domain.Comments.Events;
    using MediatR;
    using Polly;
    using Zatoichi.Common.Infrastructure.Resilience;

    public class CommentUpdatedHandler : IRequestHandler<CommentCommandUpdatedEvent, PolicyResult>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly IRepository repository;

        public CommentUpdatedHandler(
            IRepository repository,
            IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public Task<PolicyResult> Handle(CommentCommandUpdatedEvent request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.UpdateCommentAsync(request.Comment));
        }
    }
}