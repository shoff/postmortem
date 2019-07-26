
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using PostMortem.Data.MongoDb.Config;

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

    }

}