using PostMortem.Domain.Comments;

namespace PostMortem.Data.MongoDb.Converters
{
    using AutoMapper;
    using ChaosMonkey.Guards;
    using DomainComment = Domain.Comments.Comment;
    
    public class CommentConverter : ITypeConverter<DomainComment, Comment>, 
                                    ITypeConverter<Comment,DomainComment>
    {
        public Comment Convert(DomainComment source, Comment destination, ResolutionContext context)
        {
            Guard.IsNotNull(source, nameof(source));
            //Guard.IsNotNull(destination, nameof(destination)); this breaks upserts
            Guard.IsNotNull(context, nameof(context));

            destination = new Comment
            {
                Commenter = source.Commenter,
                CommentId = source.CommentId.Id,
                CommentText = source.CommentText,
                DateAdded = source.DateAdded,
                Dislikes = source.Dislikes,
                GenerallyPositive = source.GenerallyPositive,
                Likes = source.Likes,
                QuestionId = source.QuestionId
            };
            return destination;
        }

        public DomainComment Convert(Comment source, DomainComment destination, ResolutionContext context)
        {
            return new DomainComment(
                commentId:new CommentId(source.CommentId),
                questionId:source.QuestionId, 
                commenter:source.Commenter, 
                commentText:source.CommentText,
                dateAdded:source.DateAdded, 
                likes:source.Likes, 
                dislikes:source.Dislikes, 
                generallyPositive:source.GenerallyPositive);
        }
    }
}