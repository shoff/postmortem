using System.Threading;
using System.Threading.Tasks;
using ChaosMonkey.Guards;
using MediatR;
using Polly;
using PostMortem.Domain.Comments.Commands;
using PostMortem.Domain.Events.Comments;
using PostMortem.Domain.EventSourcing.Commands;
using Zatoichi.Common.Infrastructure.Resilience;

namespace PostMortem.Domain.Comments.Events
{
    public class CommentCommandHandler : 
        ICommandHandler<CreateCommentCommandArgs>,
        ICommandHandler<UpdateCommentCommandArgs>,
        ICommandHandler<AddCommentToQuestionCommandArgs>,
        ICommandHandler<LikeCommentCommandArgs>,
        ICommandHandler<DislikeCommentCommandArgs>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly ICommentRepository repository;

        public CommentCommandHandler(ICommentRepository repository, IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public async Task<PolicyResult> Handle(CreateCommentCommandArgs request, CancellationToken cancellationToken)
        {
            var comment = new Comment(request.CommentId,request.QuestionId,request.Commenter,request.CommentText,request.DateAdded,request.Likes,request.Dislikes,request.GenerallyPositive);
            return await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.SaveAsync(comment));
        }

        public async Task<PolicyResult> Handle(UpdateCommentCommandArgs request, CancellationToken cancellationToken)
        {
            var comment = new Comment(request.CommentId) {CommentText = request.CommentText};
            return await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.SaveAsync(comment));
        }

        public async Task<PolicyResult> Handle(AddCommentToQuestionCommandArgs request, CancellationToken cancellationToken)
        {
            //TODO: do we even need this?
            throw new System.NotImplementedException();
        }

        public async Task<PolicyResult> Handle(LikeCommentCommandArgs request, CancellationToken cancellationToken)
        {
            var comment = new Comment(request.CommentId);
            comment.Like();
            return await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.SaveAsync(comment));
        }

        public async Task<PolicyResult> Handle(DislikeCommentCommandArgs request, CancellationToken cancellationToken)
        {
            var comment = new Comment(request.CommentId);
            comment.Dislike();
            return await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.SaveAsync(comment));
        }
    }
}