namespace PostMortem.Domain.Comments.Events
{
    using System;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing;

    public sealed class CommentTextUpdated : DomainEvent
    {
        public CommentTextUpdated(
            Guid commentId,
            string comment,
            Guid? updatedBy) 
            : base(VersionRegistry.GetLatestVersionInformation())
        {
            this.CommentId = commentId;
            this.Comment = comment;
            this.UpdatedBy = updatedBy;
            this.EventType = this.GetType().FullName;
        }
        [JsonProperty]
        public Guid CommentId { get; private set; }
        [JsonProperty]
        public string Comment { get; private set; }
        [JsonProperty]
        public Guid? UpdatedBy { get; private set; }
        [JsonProperty]
        public override string Body { get; set; }
        [JsonProperty]
        public override string EventType { get; protected set; }
    }
}