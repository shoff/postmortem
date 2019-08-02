namespace PostMortem.Web.Converters
{
    using System;
    using AutoMapper;
    using Domain.Comments.Events;

    public class CommentByIdConverter : ITypeConverter<Guid, CommentGetByIdEvent>
    {
        public CommentGetByIdEvent Convert(Guid source, CommentGetByIdEvent destination, ResolutionContext context)
        {
            destination = new CommentGetByIdEvent(source);
            return destination;
        }
    }
}