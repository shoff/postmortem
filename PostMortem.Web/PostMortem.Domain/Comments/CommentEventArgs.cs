namespace PostMortem.Domain.Events.Comments
{
    using System;
    using ChaosMonkey.Guards;
    using Domain.Comments;
    using MediatR;

    public abstract class CommentEventArgs : EventArgs, INotification
    {
        protected CommentEventArgs (Comment comment)
        {
            this.Comment = Guard.IsNotNull(comment, nameof(comment));
        }

        public virtual DateTime CreatedDate => DateTime.UtcNow;
        public virtual Comment Comment { get; private set; }
    }
}