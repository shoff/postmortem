namespace PostMortem.Data.NEventStore
{
    using System;

    public class Project
    {
        public Guid ProjectId { get; set; }

        public string ProjectName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string CreatedBy { get; set; }

    }
}