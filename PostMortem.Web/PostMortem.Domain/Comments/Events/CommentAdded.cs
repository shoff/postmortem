namespace PostMortem.Domain.Comments.Events
{
    using System;
    using Newtonsoft.Json;

    public sealed class CommentAdded : CommentEvent
    {
        public CommentAdded()
        {
            this.EventType = this.GetType().FullName;
        }

        [JsonConstructor]
        public CommentAdded(
            string commentText,
            string commenter,
            Guid questionId,
            Guid commentId,
            Guid? parentId)
        {
            this.CommentText = commentText;
            this.VoterId = commenter;
            this.QuestionId = questionId;
            this.CommentId = commentId;
            this.ParentId = parentId;
            this.EventType = this.GetType().FullName;

        }

        [JsonProperty]
        public string CommentText { get; private set; }
        [JsonProperty]
        public Guid? ParentId { get; private set; }
    }
}