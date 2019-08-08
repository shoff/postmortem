namespace PostMortem.Domain.Projects.Events
{
    using System;
    using Newtonsoft.Json;

    public sealed class ProjectUpdated : ProjectEvent
    {
        [JsonConstructor]
        public ProjectUpdated(Guid projectId, string author, DateTime? startDate, DateTime? endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.ProjectId = projectId;
            this.Author = string.IsNullOrWhiteSpace(author) ? Constants.ANONYMOUS_COWARD : author;
            this.EventType = this.GetType().FullName;
        }
        [JsonProperty]
        public DateTime? StartDate { get; private set; }
        [JsonProperty]
        public DateTime? EndDate { get; private set; }
    }
}