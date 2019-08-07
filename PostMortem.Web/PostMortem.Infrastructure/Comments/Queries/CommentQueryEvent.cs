namespace PostMortem.Infrastructure.Comments.Queries
{
    using System;
    using Domain.Comments;
    using Zatoichi.EventSourcing.Queries;

    public abstract class CommentQueryEvent : Query<Comment>
    {
        public virtual DateTime CreatedDate => DateTime.UtcNow;
    }
}