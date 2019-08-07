namespace PostMortem.Infrastructure.Projects.Commands
{
    using System;
    using ChaosMonkey.Guards;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing;
    using Zatoichi.EventSourcing.Commands;

    public class CreateProjectCommand : Event, ICommand
    {
        public CreateProjectCommand() { }

        [JsonConstructor]
        public CreateProjectCommand(
            string projectName, 
            DateTime startDate, 
            DateTime? endDate,
            Guid? id)
        {
            this.ProjectName = Guard.IsNotNullOrWhitespace(projectName, nameof(projectName));
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Id = id;
        }
        [JsonProperty]
        public string ProjectName { get; private set; }
        [JsonProperty]
        public DateTime StartDate { get; private set; }
        [JsonProperty]
        public DateTime? EndDate { get; private set; }
        [JsonProperty]
        public Guid? Id { get; private set; }
        [JsonProperty]
        public string Description { get; set; }
    }
}