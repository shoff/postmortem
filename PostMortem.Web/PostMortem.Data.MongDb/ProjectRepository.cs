
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using PostMortem.Data.MongoDb.Config;
using PostMortem.Domain.EventSourcing.Events;

namespace PostMortem.Data.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Domain;
    using Domain.Projects;
    using DomainProject = Domain.Projects.Project;
    public class ProjectRepository : MongoDbRepositoryBase<DomainProject,ProjectId,Project,Guid>,IProjectRepository
    {
        public ProjectRepository(IMongoDbContext context, IMapper mapper, ILogger<ProjectRepository> logger) 
            : base(context?.Database, mapper, logger, Constants.PROJECTS_COLLECTION)
        {
        }

        public override ProjectId GetEntityId(Project dto) => dto.ProjectId;
        public override Guid GetDtoId(Project dto) => dto.ProjectId;
    }

}