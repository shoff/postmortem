namespace PostMortem.Infrastructure.Questions.Commands
{
    using System;
    using Domain.Questions;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing.Commands;

    public class AddQuestionCommand : ICommand
    {
        public AddQuestionCommand(Guid projectId, string questionText, string author)
        {
            this.ProjectId = projectId;
            this.QuestionText = questionText;
            this.Author = author;
            this.Description = $"{author} adding new question to project {projectId}";
        }
        [JsonProperty]
        public string Author { get; private set; }
        [JsonProperty]
        public virtual DateTime CreatedDate => DateTime.UtcNow;
        [JsonProperty]
        public string Description { get; set; }
        [JsonProperty]
        public Guid ProjectId { get; private set; }
        [JsonProperty]
        public string QuestionText { get; private set; }
        public IQuestionId QuestionId { get; set; }
    }
}