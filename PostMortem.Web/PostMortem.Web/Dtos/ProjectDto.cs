namespace PostMortem.Web.Dtos
{
    using System;
    using System.Collections.Generic;

    public class ProjectDto
    {
        private Guid projectId;
        public Guid ProjectId
        {
            get
            {
                if (this.projectId == Guid.Empty)
                {
                    this.projectId = Guid.NewGuid();
                }

                return this.projectId;
            }
            set => this.projectId = value;
        }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ICollection<QuestionDto> Questions { get; set; } = new HashSet<QuestionDto>();
    }
}