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
                CommentId = source.CommentId,
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
            return new DomainComment(source.CommentId,source.QuestionId, source.Commenter, source.CommentText,source.DateAdded, source.Likes, source.Dislikes, source.GenerallyPositive);
        }
    }
}