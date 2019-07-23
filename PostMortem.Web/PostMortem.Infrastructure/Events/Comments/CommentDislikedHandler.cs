using PostMortem.Domain.Comments;
using PostMortem.Domain.EventSourcing.Events;

namespace PostMortem.Infrastructure.Events.Comments
{
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Domain;
    using Domain.Events.Comments;
    using MediatR;
    using Polly;
    using Zatoichi.Common.Infrastructure.Resilience;

    //public class CommentDislikedHandler :  IEventHandler<CommentDislikedEventArgs>
    //{
    //    private readonly IExecutionPolicies executionPolicies;
    //    private readonly IRepository repository;
    //    private readonly EventBroker eventBroker;

    //    public CommentDislikedHandler (
    //        IRepository repository,
    //        IExecutionPolicies executionPolicies,
    //        EventBroker eventBroker)
    //    {
    //        this.eventBroker = Guard.IsNotNull(eventBroker, nameof(eventBroker));
    //        this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
    //        this.repository = Guard.IsNotNull(repository, nameof(repository));
    //    }

    //    //public Task<PolicyResult> Handle(CommentDislikedEventArgs request, CancellationToken cancellationToken)
    //    //{
    //    //    this.eventBroker.DislikeComment(this, request);
    //    //    return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.DislikeCommentAsync(request.CommentId));
    //    //}

    //    public Task Handle(CommentDislikedEventArgs notification, CancellationToken cancellationToken)
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}
}