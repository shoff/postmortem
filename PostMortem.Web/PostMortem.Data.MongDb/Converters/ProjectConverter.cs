namespace PostMortem.Data.MongoDb.Converters
{
    using System;
    using AutoMapper;
    using ChaosMonkey.Guards;

    public class ProjectConverter : ITypeConverter<Domain.Projects.Project, Project>
    {
        public Project Convert(Domain.Projects.Project source, Project destination, ResolutionContext context)
        {
            Guard.IsNotNull(source, nameof(source));
            destination = new Project
            {
                StartDate = source.StartDate,
                EndDate = source.EndDate ?? source.StartDate.AddYears(1),
                ProjectId = source.ProjectId.Id,
                ProjectName = source.ProjectName,
                CreatedBy = source.CreatedBy,
                CommitDate = DateTime.UtcNow // HACK this should be in the model
            };
            return destination;
        }
    }
}