namespace PostMortem.Domain.Comments.Events
{
    using System;
    using ChaosMonkey.Guards;
    using Comments;
    using MediatR;
    using Polly;

    public abstract class CommentCommandEvent : IRequest<PolicyResult>
    {
        protected CommentCommandEvent(Comment comment)
        {
            this.Comment = Guard.IsNotNull(comment, nameof(comment));
        }

        public virtual DateTime CreatedDate => DateTime.UtcNow;
        public virtual Comment Comment { get; private set; }
    }
}