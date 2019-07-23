using PostMortem.Data.MongoDb;
using PostMortem.Domain.Comments;
using Comment = PostMortem.Domain.Comments.Comment;

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

    public class CommentUpdatedHandler : 
        IRequestHandler<CommentCreatedEventArgs, PolicyResult>,
        IRequestHandler<CommentDislikedEventArgs, PolicyResult>,
        IRequestHandler<CommentLikedEventArgs, PolicyResult>,
        IRequestHandler<CommentGenerallyPositiveSetArgs, PolicyResult>,
        IRequestHandler<CommentTextSetArgs, PolicyResult>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly ICommentRepository repository;

        public CommentUpdatedHandler(
            ICommentRepository repository,
            IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public Task<PolicyResult> Handle(CommentCreatedEventArgs request, CancellationToken cancellationToken)
        {
            var comment = new Comment(request.CommentId,request.QuestionId,request.Commenter,request.CommentText,request.DateAdded);
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.SaveAsync(comment));
        }

        public Task<PolicyResult> Handle(CommentDislikedEventArgs request, CancellationToken cancellationToken)
        {
            var comment = new Comment(request.CommentId);
            comment.Dislike();
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.SaveAsync(comment));
        }

        public Task<PolicyResult> Handle(CommentLikedEventArgs request, CancellationToken cancellationToken)
        {
            var comment = new Comment(request.CommentId);
            comment.Like();
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.SaveAsync(comment));
        }

        public Task<PolicyResult> Handle(CommentTextSetArgs request, CancellationToken cancellationToken)
        {
            var comment = new Comment(request.CommentId);
            comment.CommentText = request.NewValue;
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.SaveAsync(comment));
        }

        public Task<PolicyResult> Handle(CommentGenerallyPositiveSetArgs request, CancellationToken cancellationToken)
        {
            var comment = new Comment(request.CommentId);
            comment.GenerallyPositive = request.NewValue;
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.SaveAsync(comment));
        }
    }
}