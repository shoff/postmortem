namespace PostMortem.Dtos
{
    using System;

    public class ProjectDto
    {
        private readonly QuestionCollection questions = new QuestionCollection();

        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public QuestionCollection Questions
        {
            get
            {
                if (this.ProjectId == Guid.Empty)
                {
                    this.ProjectId = Guid.NewGuid();
                }

                this.questions.ProjectId = this.ProjectId;
                return this.questions;
            }
        }
    }
}