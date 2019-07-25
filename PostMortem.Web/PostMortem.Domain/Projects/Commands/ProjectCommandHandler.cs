using System.Threading;
using System.Threading.Tasks;
using ChaosMonkey.Guards;
using MediatR;
using Polly;
using PostMortem.Domain.EventSourcing.Commands;
using Zatoichi.Common.Infrastructure.Resilience;

namespace PostMortem.Domain.Projects.Commands
{
    public class ProjectCommandHandler : 
        ICommandHandler<CreateProjectCommandArgs>,
        ICommandHandler<UpdateProjectDetailsCommandArgs>,
        ICommandHandler<DeleteProjectCommandArgs>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly IProjectRepository repository;
        private readonly IMediator mediator;
        public ProjectCommandHandler(
            IMediator mediator,
            IProjectRepository repository,
            IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
            this.mediator = Guard.IsNotNull(mediator, nameof(mediator));
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
            // TODO: propagate deletion to all affected dependencies. (Questions and comments)
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() =>
            {
                this.repository.DeleteByIdAsync(request.ProjectId);
                return this.repository.DeleteByIdAsync(request.ProjectId);
            });
        }
    }
}