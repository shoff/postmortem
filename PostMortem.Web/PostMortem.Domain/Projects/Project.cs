namespace PostMortem.Domain.Projects
{
    using System;
    using System.Collections.Generic;
    using Events.Projects;
    using Questions;

    public class Project
    {
        private readonly QuestionCollection questions = new QuestionCollection();

        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public IReadOnlyCollection<Question> Questions
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

        public static ProjectGetAllEventArgs CreateGetAllEventArgs()
        {
            return new ProjectGetAllEventArgs();
        }

        public static ProjectGetByIdEventArgs CreateGetByIdEventArgs(Guid projectId)
        {
            return new ProjectGetByIdEventArgs(projectId);
        }

        public static ProjectCreatedEventArgs CreateProjectCreatedEventArgs(Project project)
        {
            return new ProjectCreatedEventArgs(project);
        }
    }
}