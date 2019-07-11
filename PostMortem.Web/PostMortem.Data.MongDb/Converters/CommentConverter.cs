namespace PostMortem.Data.MongoDb.Converters
{
    using AutoMapper;
    using ChaosMonkey.Guards;
    using DomainComment = Domain.Comments.Comment;
    
    public class CommentConverter : ITypeConverter<DomainComment, Comment>
    {
        public Comment Convert(DomainComment source, Comment destination, ResolutionContext context)
        {
            Guard.IsNotNull(source, nameof(source));
            Guard.IsNotNull(destination, nameof(destination));
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
    }
}