namespace PostMortem.Infrastructure.Projects.Commands
{
    using System;
    using Domain;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing.Commands;

    public class UpdateProjectCommand : ICommand
    {
        public UpdateProjectCommand(Guid projectId, string author)
        {
            this.ProjectId = projectId;
            this.Author = string.IsNullOrWhiteSpace(author) ? Constants.ANONYMOUS_COWARD : author;
            this.Description = $"{this.Author} issued a update project command";
        }
        [JsonProperty]
        public Guid ProjectId { get; private set; }
        [JsonProperty]
        public string Author { get; private set; }
        [JsonProperty]
        public string Description { get; set; }
    }
}