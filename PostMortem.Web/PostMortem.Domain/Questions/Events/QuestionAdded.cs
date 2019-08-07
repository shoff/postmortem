namespace PostMortem.Domain.Questions.Events
{
    using System;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing;

    public sealed class QuestionAdded : DomainEvent
    {
        public QuestionAdded(Guid projectId, Guid questionId, string questionText, string author)
            : base(VersionRegistry.GetLatestVersionInformation())
        {
            this.ProjectId = projectId;
            this.QuestionText = questionText;
            this.Author = author;
            this.QuestionId = questionId;
            this.EventType = this.GetType().FullName;
        }
        [JsonProperty]
        public Guid QuestionId { get; private set; }
        [JsonProperty]
        public Guid ProjectId { get; private set; }
        [JsonProperty]
        public string QuestionText { get; private set; }
        [JsonProperty]
        public string Author { get; private set; }
        [JsonProperty]
        public override string Body { get; set; }
        [JsonProperty]
        public override string EventType { get; protected set; }
    }
}