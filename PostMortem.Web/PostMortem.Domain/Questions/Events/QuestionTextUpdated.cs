namespace PostMortem.Domain.Questions.Events
{
    using System;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing;

    public sealed class QuestionTextUpdated : DomainEvent
    {
        public QuestionTextUpdated(
            Guid questionId,
            string questionText) 
            : base(VersionRegistry.GetLatestVersionInformation())
        {
            this.QuestionId = questionId;
            this.QuestionText = questionText;
            this.CommitDateTime = DateTime.UtcNow;
            this.EventType = this.GetType().FullName;
        }

        [JsonProperty]
        public DateTime CommitDateTime { get; private set; }

        [JsonProperty]
        public Guid QuestionId { get; private set; }

        [JsonProperty]
        public string QuestionText { get; private set; }

        [JsonProperty]
        public override string Body { get; set; }

        [JsonProperty]
        public override string EventType { get; protected set; }
    }
}