namespace PostMortem.Domain.Questions.Events
{
    using System;
    using MediatR;
    using Newtonsoft.Json;

    public class QuestionTextUpdated : INotification
    {
        public QuestionTextUpdated(
            IQuestionId questionId,
            string questionText)
        {
            this.QuestionId = questionId;
            this.QuestionText = questionText;
            this.CommitDateTime = DateTime.UtcNow;
        }

        [JsonProperty]
        public DateTime CommitDateTime { get; private set; }

        [JsonProperty]
        public IQuestionId QuestionId { get; private set; }

        [JsonProperty]
        public string QuestionText { get; private set; }
    }
}