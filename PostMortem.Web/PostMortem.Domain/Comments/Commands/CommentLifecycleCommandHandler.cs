using System.Threading;
using System.Threading.Tasks;
using ChaosMonkey.Guards;
using MediatR;
using Polly;
using PostMortem.Domain.Comments.Commands;
using PostMortem.Domain.EventSourcing.Commands;
using Zatoichi.Common.Infrastructure.Resilience;

namespace PostMortem.Domain.Comments.Events
{
    public class CommentLifecycleCommandHandler : 
        ICommandHandler<CreateCommentCommandArgs>,
        ICommandHandler<DeleteCommentCommandArgs>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly ICommentRepository repository;

        public CommentLifecycleCommandHandler(ICommentRepository repository, IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public async Task<PolicyResult> Handle(CreateCommentCommandArgs request, CancellationToken cancellationToken)
        {
            var comment = new Comment(request.CommentId,request.QuestionId,request.Commenter,request.CommentText,request.DateAdded,request.Likes,request.Dislikes,request.GenerallyPositive);
            return await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.SaveAsync(comment));
        }

        public async Task<PolicyResult> Handle(DeleteCommentCommandArgs request, CancellationToken cancellationToken)
        {
            return await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.DeleteByIdAsync(request.CommentId));
        }
    }
}