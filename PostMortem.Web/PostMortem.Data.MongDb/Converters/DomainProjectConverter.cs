namespace PostMortem.Data.MongoDb.Converters
{
    using AutoMapper;
    using ChaosMonkey.Guards;

    public class DomainProjectConverter : ITypeConverter<Project, Domain.Projects.Project>
    {
        public Domain.Projects.Project Convert(Project source, Domain.Projects.Project destination, ResolutionContext context)
        {
            Guard.IsNotNull(source, nameof(source));
            destination = new Domain.Projects.Project(source.ProjectName, source.CreatedBy, source.StartDate, source.EndDate, source.ProjectId);
            return destination;
        }
    }
}