using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChaosMonkey.Guards;
using Polly;
using PostMortem.Domain.Projects;
using PostMortem.Infrastructure.Events.Projects;
using PostMortem.Infrastructure.EventSourcing.Commands;
using Zatoichi.Common.Infrastructure.Resilience;

namespace PostMortem.Domain.Questions.Commands
{
    public class QuestionCommandHandler : 
        ICommandHandler<CreateQuestionCommandArgs>,
        ICommandHandler<UpdateQuestionDetailsCommandArgs>,
        ICommandHandler<DeleteQuestionCommandArgs>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly IQuestionRepository repository;

        public QuestionCommandHandler(
            IQuestionRepository repository,
            IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public async Task<PolicyResult> Handle(CreateQuestionCommandArgs request, CancellationToken cancellationToken)
        {
            var newQuestion = new Question(request.QuestionId)
            {
                Importance = request.Importance, ProjectId = request.ProjectId, 
                Active = request.Active, QuestionText = request.QuestionText
            };
            return await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.SaveAsync(newQuestion));
        }

        public async Task<PolicyResult> Handle(UpdateQuestionDetailsCommandArgs request, CancellationToken cancellationToken)
        {
            var newQuestion = new Question(request.QuestionId)
            {
                Importance = request.Importance, ProjectId = request.ProjectId, 
                Active = request.Active, QuestionText = request.QuestionText
            };
            return await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.SaveAsync(newQuestion));
        }

        public async Task<PolicyResult> Handle(DeleteQuestionCommandArgs request, CancellationToken cancellationToken)
        {
            return await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.DeleteByIdAsync(request.QuestionId));
        }
    }
}
