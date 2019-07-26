using System.Threading;
using System.Threading.Tasks;
using ChaosMonkey.Guards;
using Polly;
using PostMortem.Domain.Events.Comments;
using PostMortem.Infrastructure.EventSourcing.Commands;
using Zatoichi.Common.Infrastructure.Resilience;

namespace PostMortem.Domain.Comments.Commands
{
    public class CommentManipulationCommandHandler : 
        ICommandHandler<UpdateCommentCommandArgs>,
        ICommandHandler<LikeCommentCommandArgs>,
        ICommandHandler<DislikeCommentCommandArgs>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly ICommentRepository repository;
        private readonly ICommentEventStoreRepository eventStore;

        public CommentManipulationCommandHandler(ICommentRepository repository, ICommentEventStoreRepository eventStore, IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
            this.eventStore = Guard.IsNotNull(eventStore, nameof(eventStore));
        }

        public Task<PolicyResult> Handle(UpdateCommentCommandArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() =>
            {
                var comment = this.eventStore.GetById(request.CommentId);
                comment.CommentText = request.CommentText;
                this.eventStore.SaveAsync(comment);
                return this.repository.SaveAsync(comment);
            });
        }

        public Task<PolicyResult> Handle(LikeCommentCommandArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() =>
            {
                var comment = this.eventStore.GetById(request.CommentId);
                comment.Like();
                this.eventStore.SaveAsync(comment);
                return this.repository.SaveAsync(comment);
            });
        }

        public Task<PolicyResult> Handle(DislikeCommentCommandArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() =>
            {
                var comment = this.eventStore.GetById(request.CommentId);
                comment.Dislike();
                this.eventStore.SaveAsync(comment);
                return this.repository.SaveAsync(comment);
            });
        }
    }
}