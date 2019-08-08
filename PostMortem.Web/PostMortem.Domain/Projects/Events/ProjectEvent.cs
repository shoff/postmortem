namespace PostMortem.Domain.Projects.Events
{
    using System;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing;

    public class ProjectEvent : DomainEvent
    {
        public ProjectEvent(string version = null)  
            : base(version ?? VersionRegistry.GetLatestVersionInformation())
        {
        }

        [JsonProperty]
        public string Author { get; protected set; }
        [JsonProperty]
        public Guid ProjectId { get; protected set; }
        [JsonProperty]
        public override string Body { get; set; }
        [JsonProperty]
        public override string EventType { get; protected set; }
    }
}