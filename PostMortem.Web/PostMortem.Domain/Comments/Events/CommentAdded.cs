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
            int maximumDisLikesPerCommentPerVoter,
            int maximumLikesPerCommentPerVoter,
            int questionMaximumLength,
            string commentText,
            string commenter,
            Guid questionId,
            Guid commentId,
            Guid? parentId)
            : base(VersionRegistry.GetLatestVersionInformation())
        {
            this.MaximumDisLikesPerCommentPerVoter = maximumDisLikesPerCommentPerVoter;
            this.MaximumLikesPerCommentPerVoter = maximumLikesPerCommentPerVoter;
            this.QuestionMaximumLength = questionMaximumLength;
            this.CommentText = commentText;
            this.Commenter = commenter;
            this.QuestionId = questionId;
            this.CommentId = commentId;
            this.ParentId = parentId;
            this.CommitDateTime = DateTime.UtcNow;
            this.EventType = this.GetType().FullName;
        }

        [JsonProperty]
        public DateTime CommitDateTime { get; private set; }
        [JsonProperty]
        public int MaximumDisLikesPerCommentPerVoter { get; private set; }
        [JsonProperty]
        public int MaximumLikesPerCommentPerVoter { get; private set; }
        [JsonProperty]
        public int QuestionMaximumLength { get; private set; }
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