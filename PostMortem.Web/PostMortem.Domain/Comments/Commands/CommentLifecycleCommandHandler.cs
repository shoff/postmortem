using System.Threading;
using System.Threading.Tasks;
using ChaosMonkey.Guards;
using Polly;
using PostMortem.Domain.Comments.Commands;
using PostMortem.Infrastructure.EventSourcing.Commands;
using Zatoichi.Common.Infrastructure.Resilience;

namespace PostMortem.Domain.Comments.Events
{
    public class CommentLifecycleCommandHandler : 
        ICommandHandler<CreateCommentCommandArgs>,
        ICommandHandler<DeleteCommentCommandArgs>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly ICommentRepository repository;
        private readonly ICommentEventStoreRepository eventStore;

        public CommentLifecycleCommandHandler(ICommentRepository repository, ICommentEventStoreRepository eventStore,IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.eventStore = Guard.IsNotNull(eventStore, nameof(eventStore));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public Task<PolicyResult> Handle(CreateCommentCommandArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() =>
            {
                var comment = new Comment(request.CommentId,request.QuestionId,request.Commenter,request.CommentText,request.DateAdded,request.Likes,request.Dislikes,request.GenerallyPositive);
                this.eventStore.SaveAsync(comment).GetAwaiter().GetResult();
                return this.repository.SaveAsync(comment);
            });
        }

        public Task<PolicyResult> Handle(DeleteCommentCommandArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() =>
            {
                //this.eventStore.DeleteByIdAsync().RunSynchronously();
                return this.repository.DeleteByIdAsync(request.CommentId);
            });
        }
    }
}