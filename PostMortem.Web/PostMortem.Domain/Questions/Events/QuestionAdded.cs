namespace PostMortem.Domain.Questions.Events
{
    using System;
    using Newtonsoft.Json;

    public sealed class QuestionAdded : QuestionEvent
    {
        public QuestionAdded(
            Guid projectId, 
            Guid questionId, 
            string questionText, 
            string author)
        {
            this.ProjectId = projectId;
            this.QuestionText = questionText;
            this.Author = author;
            this.QuestionId = questionId;
            this.EventType = this.GetType().FullName;
        }
        [JsonProperty]
        public string QuestionText { get; private set; }
    }
}