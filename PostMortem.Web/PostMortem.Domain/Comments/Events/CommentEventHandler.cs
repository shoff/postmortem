using System.Threading;
using System.Threading.Tasks;
using ChaosMonkey.Guards;
using MediatR;
using Polly;
using PostMortem.Domain.Events.Comments;
using Zatoichi.Common.Infrastructure.Resilience;

namespace PostMortem.Domain.Comments.Events
{
    //public class CommentEventHandler : 
    //    IRequestHandler<CommentCreatedEventArgs, PolicyResult>,
    //    IRequestHandler<CommentDislikedEventArgs, PolicyResult>,
    //    IRequestHandler<CommentLikedEventArgs, PolicyResult>,
    //    IRequestHandler<CommentGenerallyPositiveSetArgs, PolicyResult>,
    //    IRequestHandler<CommentTextSetArgs, PolicyResult>
    //{
    //    private readonly IExecutionPolicies executionPolicies;
    //    private readonly ICommentRepository repository;

    //    public CommentEventHandler(ICommentRepository repository, IExecutionPolicies executionPolicies)
    //    {
    //        this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
    //        this.repository = Guard.IsNotNull(repository, nameof(repository));
    //    }

    //    public async Task<PolicyResult> Handle(CommentCreatedEventArgs request, CancellationToken cancellationToken)
    //    {
    //        var comment = new Comment(request.CommentId,request.QuestionId,request.Commenter,request.CommentText,request.DateAdded,request.Likes,request.Dislikes,request.GenerallyPositive);
    //        return await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.SaveAsync(comment));
    //    }

    //    public async Task<PolicyResult> Handle(CommentDislikedEventArgs request, CancellationToken cancellationToken)
    //    {
    //        var comment = new Comment(request.CommentId);
    //        comment.Dislike();
    //        return await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.SaveAsync(comment));
    //    }

    //    public async Task<PolicyResult> Handle(CommentLikedEventArgs request, CancellationToken cancellationToken)
    //    {
    //        var comment = new Comment(request.CommentId);
    //        comment.Like();
    //        return await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.SaveAsync(comment));
    //    }

    //    public async Task<PolicyResult> Handle(CommentTextSetArgs request, CancellationToken cancellationToken)
    //    {
    //        var comment = new Comment(request.CommentId);
    //        comment.CommentText = request.NewValue;
    //        return await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.SaveAsync(comment));
    //    }

    //    public async Task<PolicyResult> Handle(CommentGenerallyPositiveSetArgs request, CancellationToken cancellationToken)
    //    {
    //        var comment = new Comment(request.CommentId);
    //        comment.GenerallyPositive = request.NewValue;
    //        return await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.SaveAsync(comment));
    //    }
    //}
}