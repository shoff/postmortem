using System.Collections.Generic;
using System.Threading.Tasks;
using PostMortem.Domain.Projects;

namespace PostMortem.Domain.Questions
{
    public interface IQuestionRepository: IRepository<Question, QuestionId>
    {
        Task<IEnumerable<Question>> GetQuestionsForProjectAsync(ProjectId projectId);
    }
}