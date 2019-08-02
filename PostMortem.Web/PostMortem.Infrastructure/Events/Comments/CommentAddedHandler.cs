namespace PostMortem.Infrastructure.Events.Comments
{
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Domain;
    using Domain.Comments.Events;
    using MediatR;
    using Zatoichi.Common.Infrastructure.Resilience;

    public class CommentAddedHandler : INotificationHandler<CommentCommandAddedEvent>
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

        //public Task<PolicyResult<Guid>> Handle(CommentCommandAddedEvent request, CancellationToken cancellationToken)
        //{
        //    return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync (()=> this.repository.AddCommentAsync(request.Comment));
        //}

        public Task Handle(CommentCommandAddedEvent notification, CancellationToken cancellationToken)
        {
            this.executionPolicies.DbExecutionPolicy.ExecuteAsync(() => this.repository.AddCommentAsync(notification.Comment));
            // TODO right here is possibly where we could raise a rabbit/kafka event to notify others of what just happened
            return Task.CompletedTask;
        }
    }
}