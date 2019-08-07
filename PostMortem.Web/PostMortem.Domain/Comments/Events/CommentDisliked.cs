namespace PostMortem.Domain.Comments.Events
{
    using System;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing;

    public sealed class CommentDisliked : DomainEvent
    {
        public CommentDisliked(Guid commentId, string voterId = null)
            : base(VersionRegistry.GetLatestVersionInformation())

        {
            this.CommentId = commentId;
            this.VoterId = string.IsNullOrWhiteSpace(voterId) ? Constants.ANONYMOUS_COWARD : voterId;
            this.EventType = this.GetType().FullName;
        }

        [JsonProperty]
        public Guid CommentId { get; private set; }

        [JsonProperty]
        public string VoterId { get; private set; }

        [JsonProperty]
        public override string Body { get; set; }

        [JsonProperty]
        public override string EventType { get; protected set; }
    }
}