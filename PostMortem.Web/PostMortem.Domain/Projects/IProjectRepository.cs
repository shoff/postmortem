using PostMortem.Domain;
using PostMortem.Domain.Projects;

namespace PostMortem.Data.MongoDb
{
    public interface IProjectRepository: IRepository<Domain.Projects.Project, ProjectId>
    {
    }
}