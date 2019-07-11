namespace PostMortem.Data.MongoDb.Converters
{
    using AutoMapper;
    using ChaosMonkey.Guards;
    using DomainProject = Domain.Projects.Project;
    using Project = Project;

    public class ProjectConverter : ITypeConverter<DomainProject, Project>
    {
        public Project Convert(DomainProject source, Project destination, ResolutionContext context)
        {
            Guard.IsNotNull(source, nameof(source));
            Guard.IsNotNull(destination, nameof(destination));
            Guard.IsNotNull(context, nameof(context));

            destination = new Project
            {
                EndDate = source.EndDate,
                ProjectId = source.ProjectId,
                ProjectName = source.ProjectName
            };
            return destination;
        }
    }
}