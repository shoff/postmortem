namespace PostMortem.Dtos
{
    using System;
    using System.Collections.Generic;

    public class ProjectDto
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ICollection<QuestionDto> Questions { get; set; } = new HashSet<QuestionDto>();
    }
}