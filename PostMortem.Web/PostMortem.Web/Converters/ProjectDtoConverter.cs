using System.Collections.Generic;

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
            //Guard.IsNotNull(destination, nameof(destination));
            Guard.IsNotNull(context, nameof(context));

            destination = new ProjectDto
            {
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                ProjectId = source.ProjectId,
                ProjectName = source.ProjectName,
                Questions = source.Questions?.Select(q=> context.Mapper.Map<QuestionDto>(q)).ToList() ?? new List<QuestionDto>()
            };
            return destination;
        }
    }
}