namespace PostMortem.Domain.Comments.Events
{
    using System;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing;

    public sealed class CommentAdded : DomainEvent
    {
        public CommentAdded()
            : base(VersionRegistry.GetLatestVersionInformation())
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
            : base(VersionRegistry.GetLatestVersionInformation())
        {
            this.CommentText = commentText;
            this.Commenter = commenter;
            this.QuestionId = questionId;
            this.CommentId = commentId;
            this.ParentId = parentId;
            this.EventType = this.GetType().FullName;
        }

        [JsonProperty]
        public string CommentText { get; private set; }
        [JsonProperty]
        public string Commenter { get; private set; }
        [JsonProperty]
        public Guid QuestionId { get; private set; }
        [JsonProperty]
        public Guid CommentId { get; private set; }
        [JsonProperty]
        public Guid? ParentId { get; private set; }
        [JsonProperty]
        public override string Body { get; set; }
        [JsonProperty]
        public override string EventType { get; protected set; }
    }
}