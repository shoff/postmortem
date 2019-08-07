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
            // TODO configure default project length
            destination = new Project
            {
                StartDate = source.StartDate,
                EndDate = source.EndDate ?? source.StartDate.AddYears(1),
                ProjectId = source.ProjectId.Id,
                ProjectName = source.ProjectName
            };
            return destination;
        }
    }
}