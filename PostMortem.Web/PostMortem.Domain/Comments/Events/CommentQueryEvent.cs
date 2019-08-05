﻿namespace PostMortem.Domain.Comments.Events
{
    using System;
    using Zatoichi.EventSourcing.Queries;

    public abstract class CommentQueryEvent : Query<Comment>
    {
        public virtual DateTime CreatedDate => DateTime.UtcNow;
    }
}