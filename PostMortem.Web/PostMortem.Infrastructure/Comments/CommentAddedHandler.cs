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

    public class CommentAddedHandler : IRequestHandler<CommentCommandAddedEvent, PolicyResult>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly IRepository repository;

        public CommentAddedHandler(
            IRepository repository,
            IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }


        public async Task<PolicyResult> Handle(CommentCommandAddedEvent request, CancellationToken cancellationToken)
        {
            var result = await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.AddCommentAsync(request.Comment));
            // TODO right here is possibly where we could raise a rabbit/kafka event to notify others of what just happened
            return PolicyResult.Successful(result.Context);
        }
    }
}