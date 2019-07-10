namespace PostMortem.Domain.Events.Projects
{
    using ChaosMonkey.Guards;
    using Domain.Projects;

    public class ProjectCreatedEventArgs
    {
        public ProjectCreatedEventArgs(Project project)
        {
            this.Project = Guard.IsNotNull(project, nameof(project));
        }

        public Project Project { get; private set; }
    }
}