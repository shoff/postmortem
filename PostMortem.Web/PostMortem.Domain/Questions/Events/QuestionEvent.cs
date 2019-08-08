namespace PostMortem.Domain.Questions.Events
{
    using System;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing;

    public class QuestionEvent : DomainEvent
    {
        public QuestionEvent(string version = null) 
            : base(version ?? VersionRegistry.GetLatestVersionInformation())
        {
        }
        [JsonProperty]
        public Guid ProjectId { get; protected set; }
        [JsonProperty]
        public Guid QuestionId { get; protected set; }
        [JsonProperty]
        public override string Body { get; set; }
        [JsonProperty]
        public override string EventType { get; protected set; }
        [JsonProperty]
        public string Author { get; protected set; }
    }
}