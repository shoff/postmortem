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

    //public class CommentLikedHandler : IEventHandler<CommentLikedEventArgs>
    //{
    //    private readonly IExecutionPolicies executionPolicies;
    //    private readonly IRepository repository;

    //    public CommentLikedHandler(
    //        IRepository repository,
    //        IExecutionPolicies executionPolicies)
    //    {
    //        this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
    //        this.repository = Guard.IsNotNull(repository, nameof(repository));
    //    }

    //    //public Task<PolicyResult> Handle(CommentLikedEventArgs request, CancellationToken cancellationToken)
    //    //{
    //    //    return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.LikeCommentAsync(request.CommentId));
    //    //}

    //    public Task Handle(CommentLikedEventArgs notification, CancellationToken cancellationToken)
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}
}