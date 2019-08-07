namespace PostMortem.Infrastructure.Questions.Commands
{
    using System;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing;
    using Zatoichi.EventSourcing.Commands;

    public abstract class QuestionCommand : Event, ICommand
    {
        protected QuestionCommand(Guid questionId, Guid projectId, string questionText)
        {
            this.QuestionId = questionId;
            this.ProjectId = projectId;
            this.QuestionText = questionText;
        }

        [JsonProperty]
        public virtual DateTime CreatedDate => DateTime.UtcNow;
        [JsonProperty]
        public string Description { get; set; } = "Add a question to a project.";
        [JsonProperty]
        public Guid QuestionId { get; private set; }
        [JsonProperty]
        public Guid ProjectId { get; private set; }
        [JsonProperty]
        public string QuestionText { get; private set; }
    }


}