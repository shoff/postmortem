namespace PostMortem.Infrastructure.Events.Comments
{
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Domain;
    using Domain.Events;
    using Domain.Events.Comments;
    using MediatR;
    using Polly;
    using Zatoichi.Common.Infrastructure.Extensions;
    using Zatoichi.Common.Infrastructure.Resilience;

    public class CommentDislikedHandler : IRequestHandler<CommentDislikedEventArgs, PolicyResult>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly IRepository repository;
        private readonly IEventBroker eventBroker;

        public CommentDislikedHandler (
            IRepository repository,
            IExecutionPolicies executionPolicies,
            IEventBroker eventBroker)
        {
            this.eventBroker = Guard.IsNotNull(eventBroker, nameof(eventBroker));
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
            
        }

        public Task<PolicyResult> Handle(CommentDislikedEventArgs request, CancellationToken cancellationToken)
        {
            //this.eventBroker.CommentDisliked.Raise(this, request);
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.DislikeCommentAsync(request.CommentId));
        }
    }
}