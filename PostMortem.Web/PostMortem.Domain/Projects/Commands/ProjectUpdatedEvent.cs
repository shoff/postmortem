namespace PostMortem.Domain.Projects.Commands
{
    using ChaosMonkey.Guards;
    using MediatR;
    using Polly;
    using Projects;

    public class ProjectUpdatedEvent : IRequest<PolicyResult>
    {
        public ProjectUpdatedEvent(Project project)
        {
            this.Project = Guard.IsNotNull(project, nameof(project));
        }

        public Project Project { get; private set; }
    }
}