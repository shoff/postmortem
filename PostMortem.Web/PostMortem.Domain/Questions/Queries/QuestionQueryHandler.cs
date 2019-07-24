using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChaosMonkey.Guards;
using Polly;
using PostMortem.Domain.Comments;
using PostMortem.Domain.EventSourcing.Queries;
using Zatoichi.Common.Infrastructure.Resilience;

namespace PostMortem.Domain.Questions.Queries
{
    public class QuestionQueryHandler :
        IQueryHandler<GetAllQuestionsQueryArgs,IEnumerable<Question>>,
        IQueryHandler<GetQuestionByIdQueryArgs,Question>,
        IQueryHandler<GetQuestionsForProjectIdQueryArgs,IEnumerable<Question>>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly IQuestionRepository repository;

        public QuestionQueryHandler(
            IQuestionRepository repository,
            IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public Task<PolicyResult<IEnumerable<Question>>> Handle(GetAllQuestionsQueryArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.GetAllAsync());
        }

        public Task<PolicyResult<Question>> Handle(GetQuestionByIdQueryArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.GetByIdAsync(request.QuestionId));
        }

        public Task<PolicyResult<IEnumerable<Question>>> Handle(GetQuestionsForProjectIdQueryArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.GetQuestionsForProjectAsync(request.ProjectId));
        }
    }
}
