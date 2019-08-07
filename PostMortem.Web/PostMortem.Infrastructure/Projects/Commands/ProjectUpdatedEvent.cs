namespace PostMortem.Infrastructure.Projects.Commands
{
    using ChaosMonkey.Guards;
    using Domain.Projects;
    using MediatR;
    using Polly;

    public class ProjectUpdatedEvent : IRequest<PolicyResult>
    {
        public ProjectUpdatedEvent(Project project)
        {
            this.Project = Guard.IsNotNull(project, nameof(project));
        }

        public Project Project { get; private set; }
    }
}