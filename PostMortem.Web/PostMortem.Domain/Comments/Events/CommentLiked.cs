namespace PostMortem.Domain.Comments.Events
{
    using System;

    public sealed class CommentLiked : CommentEvent
    {
        public CommentLiked(Guid commentId, Guid questionId, string voterId = null)
        {
            this.CommentId = commentId;
            this.QuestionId = questionId;
            this.VoterId = string.IsNullOrWhiteSpace(voterId) ? Constants.ANONYMOUS_COWARD : voterId;
            this.EventType = this.GetType().FullName;
        }
    }
}