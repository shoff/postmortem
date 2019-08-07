namespace PostMortem.Infrastructure.Questions.Commands
{
    using System;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing.Commands;

    public class DeleteQuestionCommand : ICommand
    {
        public DeleteQuestionCommand(
            Guid questionId,
            string author)
        {
            this.QuestionId = questionId;
            this.Author = author;
            this.Description = $"Marking question with Id {questionId} as deleted.";
        }

        [JsonProperty]
        public Guid QuestionId { get; private set; }
        [JsonProperty]
        public string Author { get; private set; }
        [JsonProperty]
        public string Description { get; set; }
    }
}