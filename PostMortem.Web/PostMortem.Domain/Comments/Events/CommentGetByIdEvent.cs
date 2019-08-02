﻿namespace PostMortem.Domain.Comments.Events
{
    using System;

    public class CommentGetByIdEvent : CommentQueryEvent
    {
        public Guid CommentId { get; }

        public CommentGetByIdEvent(Guid commentId)
        {
            CommentId = commentId;
        }
    }
}