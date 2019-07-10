namespace PostMortem.Domain.Events.Comments
{
    using System;
    using MediatR;

    public class CommentLikedEventArgs : EventArgs, INotification
    {
        public Guid CommentId { get; set; } 
    }
}