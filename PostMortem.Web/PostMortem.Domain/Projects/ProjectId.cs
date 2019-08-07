namespace PostMortem.Domain.Projects
{
    using System;
    using Newtonsoft.Json;

    public class ProjectId : IProjectId
    {
        public ProjectId(Guid id)
        {
            this.Id = id;
        }

        [JsonProperty]
        public Guid Id { get; private set; }
    }
}