using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChaosMonkey.Guards;
using MediatR;
using Polly;
using PostMortem.Domain.Comments;
using PostMortem.Domain.Comments.Queries;
using PostMortem.Infrastructure.Queries;
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
        private readonly IMediator mediator;

        public QuestionQueryHandler(
            IQuestionRepository repository,
            IExecutionPolicies executionPolicies,
            IMediator mediator
            )
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
            this.mediator = Guard.IsNotNull(mediator, nameof(mediator));
        }

        public async Task<PolicyResult<IEnumerable<Question>>> Handle(GetAllQuestionsQueryArgs request, CancellationToken cancellationToken)
        {
            // TODO: add a flag to the args to indicate if we want the questions fully filled in.
            var policyResult= await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.GetAllAsync());
            if (policyResult.Outcome == OutcomeType.Successful && policyResult.Result != null)
            {
                await PopulateComments(policyResult.Result);
            }
            return policyResult;
        }

        public async Task<PolicyResult<Question>> Handle(GetQuestionByIdQueryArgs request, CancellationToken cancellationToken)
        {
            // TODO: add a flag to the args to indicate if we want the questions fully filled in.
            var policyResult= await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.GetByIdAsync(request.QuestionId));
            if (policyResult.Outcome == OutcomeType.Successful && policyResult.Result != null)
            {
                await PopulateComments(policyResult.Result);
            }
            return policyResult;
        }

        public async Task<PolicyResult<IEnumerable<Question>>> Handle(GetQuestionsForProjectIdQueryArgs request, CancellationToken cancellationToken)
        {
            // TODO: add a flag to the args to indicate if we want the questions fully filled in.
            var policyResult= await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.GetQuestionsForProjectAsync(request.ProjectId));
            if (policyResult.Outcome == OutcomeType.Successful && policyResult.Result != null)
            {
                await PopulateComments(policyResult.Result);
            }

            return policyResult;
        }
        private async Task PopulateComments(Question question)
        {
            var commentsResult=await mediator.Send(new GetCommentsForQuestionQueryArgs {QuestionId = question.QuestionId});
            if (commentsResult.Outcome==OutcomeType.Successful && commentsResult.Result!=null)
            {
                question.AttachComments(commentsResult.Result);
            }
        }

        private async Task PopulateComments(IEnumerable<Question> questions)
        {
            // TODO: get all comments in one go, and use linq on in memory collection to resolve the associations?
            // For now just populate one at a time.
            foreach (var question in questions)
            {
                await PopulateComments(question);
            }
        }
    }
}
