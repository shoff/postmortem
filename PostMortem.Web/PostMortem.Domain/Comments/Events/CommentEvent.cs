namespace PostMortem.Domain.Comments.Events
{
    using System;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing;

    public class CommentEvent : DomainEvent
    {
        public CommentEvent(string version = null)
            : base(version ?? VersionRegistry.GetLatestVersionInformation())
        {
        }
        [JsonProperty]
        public string Expression { get; set; }
        [JsonProperty]
        public Guid CommentId { get; protected set; }
        [JsonProperty]
        public Guid QuestionId { get; protected set; }
        [JsonProperty]
        public override string Body { get; set; }
        [JsonProperty]
        public override string EventType { get; protected set; }
        [JsonProperty]
        public string VoterId { get; protected set; }
    }
}