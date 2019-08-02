namespace PostMortem.Infrastructure.Factories
{
    using System;
    using AutoMapper;
    using Domain;
    using Domain.Comments;
    using Domain.Comments.Events;

    public class EventFactory : IEventFactory
    {
        private readonly IMapper mapper;

        public EventFactory(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public CommentGetByIdEvent CreateEvent(Guid commentId)
        {
            return new CommentGetByIdEvent(commentId);
        }

        public CommentCommandAddedEvent CreateEvent(Comment comment)
        {
            return new CommentCommandAddedEvent(comment);
        }
    }
}