namespace PostMortem.Data.EventSourcing
{
    using System;

    public class Project
    {
        public Guid ProjectId { get; private set; }
        public string ProjectName { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
    }
}