namespace PostMortem.Data.MongoDb.Converters
{
    using AutoMapper;
    using ChaosMonkey.Guards;
    using DomainProject = Domain.Projects.Project;

    public class DomainProjectConverter : ITypeConverter<Project, DomainProject>
    {
        public DomainProject Convert(Project source, DomainProject destination, ResolutionContext context)
        {
            Guard.IsNotNull(source, nameof(source));
            destination = new DomainProject(source.ProjectName, source.CreatedBy, source.StartDate, source.EndDate, source.ProjectId);
            return destination;
        }
    }
}