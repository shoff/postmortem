namespace PostMortem.Domain.Questions.Events
{
    using System;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing;

    public sealed class QuestionUpdated : DomainEvent
    {
        public QuestionUpdated(
            Guid questionId,
            string questionText,
            string author) 
            : base(VersionRegistry.GetLatestVersionInformation())
        {
            this.QuestionId = questionId;
            this.QuestionText = questionText;
            this.CommitDateTime = DateTime.UtcNow;
            this.Author = string.IsNullOrWhiteSpace(author) ? Constants.ANONYMOUS_COWARD : author;
            this.EventType = this.GetType().FullName;
        }

        [JsonProperty]
        public string Author { get; private set; }

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