namespace PostMortem.Domain.Comments.Events
{
    using System;
    using MediatR;
    using Polly;

    public abstract class CommentQueryEvent : DomainEvent, IRequest<PolicyResult<Comment>>
    {
        public virtual DateTime CreatedDate => DateTime.UtcNow;
    }
}