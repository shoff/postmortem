namespace PostMortem.Web.Converters
{
    using System.Linq;
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Domain.Projects;
    using Dtos;
    using Zatoichi.Common.Infrastructure.Extensions;

    public class ProjectDtoConverter : ITypeConverter<Project, ProjectDto>
    {
        public ProjectDto Convert(Project source, ProjectDto destination, ResolutionContext context)
        {
            Guard.IsNotNull(source, nameof(source));
            Guard.IsNotNull(destination, nameof(destination));
            Guard.IsNotNull(context, nameof(context));

            destination = new ProjectDto
            {
                EndDate = source.EndDate,
                ProjectId = source.ProjectId,
                ProjectName = source.ProjectName,
                Questions = source.Questions.Map(q=> context.Mapper.Map<QuestionDto>(q)).ToList()
            };
            return destination;
        }
    }
}