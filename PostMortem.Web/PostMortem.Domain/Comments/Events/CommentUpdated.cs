namespace PostMortem.Domain.Comments.Events
{
    using System;
    using Newtonsoft.Json;

    public sealed class CommentUpdated : CommentEvent
    {
        public CommentUpdated(
            Guid commentId,
            Guid questionId,
            string comment,
            string author = null) 
        {
            this.CommentId = commentId;
            this.QuestionId = questionId;
            this.Comment = comment;
            this.VoterId = string.IsNullOrWhiteSpace(author) ? Constants.ANONYMOUS_COWARD : author;
            this.EventType = this.GetType().FullName;
        }

        [JsonProperty]
        public string Comment { get; private set; }
    }
}