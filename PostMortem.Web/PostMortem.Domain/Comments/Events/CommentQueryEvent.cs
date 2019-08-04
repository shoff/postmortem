namespace PostMortem.Domain.Comments.Events
{
    using System;
    using MediatR;
    using Polly;
    using Zatoichi.EventSourcing;

    public abstract class CommentQueryEvent : Query<IQuery>, IRequest<PolicyResult<Comment>>
    {
        public virtual DateTime CreatedDate => DateTime.UtcNow;
    }
}