namespace PostMortem.Domain.Events.Comments
{
    using System;
    using ChaosMonkey.Guards;
    using Domain.Comments;
    using Domain.Questions;
    using MediatR;

    public abstract class CommentEventArgs : EventArgs, INotification
    {
        protected CommentEventArgs(Question question, Comment comment)
        {
            this.Comment = Guard.IsNotNull(comment, nameof(comment));
            this.Question = Guard.IsNotNull(question, nameof(question));
        }

        public virtual DateTime CreatedDate => DateTime.UtcNow;
        public virtual Comment Comment { get; private set; }
        public virtual Question Question { get; private set; }
    }
}