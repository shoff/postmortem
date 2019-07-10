namespace PostMortem.Domain.Events.Comments
{
    using System;
    using MediatR;

    public class CommentDislikedEventArgs : EventArgs, INotification
    {
        public Guid CommentId { get; set; }
    }
}