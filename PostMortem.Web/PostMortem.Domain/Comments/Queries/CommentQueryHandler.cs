using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChaosMonkey.Guards;
using MediatR;
using Polly;
using PostMortem.Domain.Comments.Queries;
using PostMortem.Domain.Events.Comments;
using PostMortem.Infrastructure.Queries;
using Zatoichi.Common.Infrastructure.Resilience;

namespace PostMortem.Domain.Comments.Events
{
    public class CommentQueryHandler : 
        IQueryHandler<GetAllCommentsQueryArgs,IEnumerable<Comment>>,
        IQueryHandler<GetCommentsForQuestionQueryArgs,IEnumerable<Comment>>,
        IQueryHandler<GetCommentByIdQueryArgs,Comment>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly ICommentRepository repository;
        private readonly ICommentEventStoreRepository eventStore;
        public CommentQueryHandler(ICommentRepository repository, ICommentEventStoreRepository eventStore,IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
            this.eventStore = Guard.IsNotNull(eventStore, nameof(eventStore));
        }

        public Task<PolicyResult<IEnumerable<Comment>>> Handle(GetAllCommentsQueryArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(async () =>
            {
                var result= await this.repository.GetAllAsync();
                return ReplayEventsForComments(result);
            });
        }

        private IEnumerable<Comment> ReplayEventsForComments(IEnumerable<Comment> result)
        {
            var list = result.ToList();
            foreach (var comment in list)
            {
                var events = eventStore.LoadEvents(comment.CommentId).ToList();
                yield return new Comment(comment.CommentId, events);
            }
        }

        public Task<PolicyResult<IEnumerable<Comment>>> Handle(GetCommentsForQuestionQueryArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(async () =>
            {
                var result=await this.repository.GetCommentsForQuestionAsync(request.QuestionId);
                return ReplayEventsForComments(result);
            });
        }

        public Task<PolicyResult<Comment>> Handle(GetCommentByIdQueryArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => Task.Run(()=>this.eventStore.GetById(request.CommentId)));
        }
    }
}