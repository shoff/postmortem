using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChaosMonkey.Guards;
using MediatR;
using Polly;
using PostMortem.Domain.Comments.Queries;
using PostMortem.Domain.Events.Comments;
using PostMortem.Domain.EventSourcing.Queries;
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

        public CommentQueryHandler(ICommentRepository repository, IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public Task<PolicyResult<IEnumerable<Comment>>> Handle(GetAllCommentsQueryArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.GetAllAsync());
        }

        public Task<PolicyResult<IEnumerable<Comment>>> Handle(GetCommentsForQuestionQueryArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.GetCommentsForQuestionAsync(request.QuestionId));
        }

        public Task<PolicyResult<Comment>> Handle(GetCommentByIdQueryArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.GetByIdAsync(request.CommentId));
        }
    }
}