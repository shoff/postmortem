namespace PostMortem.Domain.Projects.Events
{
    using ChaosMonkey.Guards;
    using MediatR;
    using Polly;
    using Projects;

    public class ProjectUpdatedEventArgs : IRequest<PolicyResult>
    {
        public ProjectUpdatedEventArgs(Project project)
        {
            this.Project = Guard.IsNotNull(project, nameof(project));
        }

        public Project Project { get; private set; }
    }
}