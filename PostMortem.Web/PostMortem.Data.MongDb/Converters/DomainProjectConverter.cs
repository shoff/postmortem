namespace PostMortem.Data.MongoDb.Converters
{
    using AutoMapper;
    using ChaosMonkey.Guards;
    using DomainProject = Domain.Projects.Project;

    public class DomainProjectConverter : ITypeConverter<Project, DomainProject>
    {
        public Domain.Projects.Project Convert(Project source, DomainProject destination, ResolutionContext context)
        {
            Guard.IsNotNull(source, nameof(source));
            Guard.IsNotNull(destination, nameof(destination));
            Guard.IsNotNull(context, nameof(context));
            destination = new DomainProject(source.ProjectName, source.StartDate, source.EndDate, source.ProjectId);
            return destination;
        }
    }
}