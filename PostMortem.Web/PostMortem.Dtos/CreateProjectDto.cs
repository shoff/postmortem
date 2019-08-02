namespace PostMortem.Web.Dtos
{
    using System;
    using Newtonsoft.Json;

    public class CreateProjectDto
    {
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime? EndDate { get; set; }

        // let our controller handle this
        [JsonIgnore]
        public string CreatedBy { get; set; }
    }
}