namespace PostMortem.Infrastructure.Questions.Commands
{
    using System;
    using Domain;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing.Commands;

    public class UpdateQuestionCommand : ICommand
    {
        public UpdateQuestionCommand(Guid questionId, Guid projectId, string questionText, string author = null)
        {
            this.QuestionId = questionId;
            this.ProjectId = projectId;
            this.QuestionText = questionText;
            this.Author = string.IsNullOrWhiteSpace(author) ? Constants.ANONYMOUS_COWARD : author;
            this.Description = $"{this.Author} updating question {questionId}";
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
        public string Description { get; set; }
    }
}