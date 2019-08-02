namespace PostMortem.Domain.Comments.Events
{
    using System;
    using ChaosMonkey.Guards;
    using Comments;
    using MediatR;

    public abstract class CommentCommandEvent : DomainEvent, INotification
    {
        protected CommentCommandEvent(Comment comment)
        {
            this.Comment = Guard.IsNotNull(comment, nameof(comment));
        }

        public virtual DateTime CreatedDate => DateTime.UtcNow;
        public virtual Comment Comment { get; private set; }
    }
}