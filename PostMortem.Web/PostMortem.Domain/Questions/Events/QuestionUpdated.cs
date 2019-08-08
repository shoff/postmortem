namespace PostMortem.Domain.Questions.Events
{
    using System;
    using Newtonsoft.Json;

    public sealed class QuestionUpdated : QuestionEvent
    {
        public QuestionUpdated(
            Guid questionId,
            Guid projectId,
            string questionText,
            string author)
        {
            this.QuestionId = questionId;
            this.ProjectId = projectId;
            this.QuestionText = questionText;
            this.Author = string.IsNullOrWhiteSpace(author) ? Constants.ANONYMOUS_COWARD : author;
            this.EventType = this.GetType().FullName;
        }

        [JsonProperty]
        public string QuestionText { get; private set; }
    }
}