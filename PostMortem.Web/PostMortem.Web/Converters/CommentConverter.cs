namespace PostMortem.Web.Converters
{
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Domain.Comments;
    using Dtos;

    public class CommentConverter : ITypeConverter<Comment, CommentDto>
    {
        public CommentDto Convert(Comment source, CommentDto destination, ResolutionContext context)
        {
            Guard.IsNotNull(source, nameof(source));
            Guard.IsNotNull(destination, nameof(destination));

            return destination;
        }
    }
}