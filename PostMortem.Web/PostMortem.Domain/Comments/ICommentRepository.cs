using System.Collections.Generic;
using System.Threading.Tasks;
using PostMortem.Domain.Projects;
using PostMortem.Domain.Questions;

namespace PostMortem.Domain.Comments
{
    public interface ICommentRepository: IRepository<Domain.Comments.Comment, CommentId>
    {
        Task<IEnumerable<Comment>> GetCommentsForQuestionAsync(QuestionId questionId);
    }
}
