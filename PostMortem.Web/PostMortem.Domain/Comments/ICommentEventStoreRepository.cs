using PostMortem.Domain.Comments.Events;
using PostMortem.Infrastructure.Events;


namespace PostMortem.Domain.Comments
{
    public interface ICommentEventStoreRepository : IEventStoreRepository<Comment,CommentId,CommentEventArgsBase>
    {
    }
}
