using ChaosMonkey.Guards;
using PostMortem.Domain.EventSourcing.Commands;
using PostMortem.Domain.Projects;
using PostMortem.Domain.Projects.Commands;
using Zatoichi.Common.Infrastructure.Resilience;

namespace PostMortem.Infrastructure.Events.Projects
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Polly;

    public class ProjectCommandHandler : 
        ICommandHandler<CreateProjectCommandArgs>,
        ICommandHandler<UpdateProjectDetailsCommandArgs>,
        ICommandHandler<DeleteProjectCommandArgs>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly IProjectRepository repository;

        public ProjectCommandHandler(
            IProjectRepository repository,
            IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public Task<PolicyResult> Handle(UpdateProjectDetailsCommandArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.SaveAsync(new Project {EndDate = request.EndDate, ProjectId = request.ProjectId, ProjectName = request.ProjectName, StartDate = request.StartDate}));
        }

        public Task<PolicyResult> Handle(CreateProjectCommandArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.SaveAsync(new Project {EndDate = request.EndDate, ProjectId = request.ProjectId, ProjectName = request.ProjectName, StartDate = request.StartDate}));
        }

        public Task<PolicyResult> Handle(DeleteProjectCommandArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.DeleteByIdAsync(request.ProjectId));
        }
    }
}