namespace PostMortem.Web.Converters
{
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Domain.Comments;
    using Dtos;

    public class CommentDtoConverter : ITypeConverter<Comment, CommentDto>
    {
       public CommentDto Convert(Comment source, CommentDto destination, ResolutionContext context)
        {
            Guard.IsNotNull(source, nameof(source));
            Guard.IsNotNull(destination, nameof(destination));
            Guard.IsNotNull(context, nameof(context));
            
            destination = new CommentDto
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