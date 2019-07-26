using System.Collections.Generic;
using PostMortem.Domain.Comments;
using PostMortem.Domain.Comments.Queries;
using PostMortem.Domain.Projects;
using PostMortem.Domain.Questions;
using PostMortem.Domain.Questions.Queries;
using PostMortem.Infrastructure.Queries;

namespace PostMortem.Infrastructure.Events.Projects
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Domain;
    using MediatR;
    using Polly;
    using Zatoichi.Common.Infrastructure.Resilience;

    public class ProjectQueryHandler : 
        IQueryHandler<GetAllProjectsQueryArgs,IEnumerable<Project>>,
        IQueryHandler<GetProjectByIdQueryArgs,Project>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly IProjectRepository projectRepository;
        private readonly IMediator mediator;

        public ProjectQueryHandler(
            IMediator mediator,
            IProjectRepository projectRepository,
            IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.projectRepository = Guard.IsNotNull(projectRepository, nameof(projectRepository));
            this.mediator = Guard.IsNotNull(mediator, nameof(mediator));
        }

        public async Task<PolicyResult<IEnumerable<Project>>> Handle(GetAllProjectsQueryArgs request, CancellationToken cancellationToken)
        {
            //TODO: optionally hydrate all projects.
            var policyResult=await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.projectRepository.GetAllAsync());
            if (policyResult.Outcome == OutcomeType.Successful && policyResult.Result != null)
            {
                await PopulateQuestions(policyResult.Result);
            }

            return policyResult;
        }

        public async Task<PolicyResult<Project>> Handle(GetProjectByIdQueryArgs request, CancellationToken cancellationToken)
        {
            // TODO: do error checking.
            var policyResult = await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.projectRepository.GetByIdAsync(request.ProjectId));
            if (policyResult.Outcome == OutcomeType.Successful && policyResult.Result != null)
            {
                await PopulateQuestions(policyResult.Result);
            }
            return policyResult;
        }

        private async Task PopulateQuestions(Project project)
        {
            var questionsResult=await mediator.Send(new GetQuestionsForProjectIdQueryArgs {ProjectId = project.ProjectId});
            if (questionsResult.Outcome==OutcomeType.Successful && questionsResult.Result!=null)
            {
                project.AttachQuestions(questionsResult.Result);
            }
        }

        private async Task PopulateQuestions(IEnumerable<Project> projects)
        {
            // TODO: get all questions in one go, and use linq on in memory collection to resolve the associations?
            // For now just populate one at a time.
            foreach (var project in projects)
            {
                await PopulateQuestions(project);
            }
        }

    }
}