namespace PostMortem.Domain.Projects
{
    using System;
    using System.Collections.Generic;
    using ChaosMonkey.Guards;
    using Events.Projects;
    using Questions;

    public class Project
    {
        private readonly QuestionCollection questions = new QuestionCollection();

        public Project()
            : this(new List<Question>())
        {
        }

        public Project(ICollection<Question> questions)
        {
            Guard.IsNotNull(questions, nameof(questions));
            this.questions.AddRange(questions);
        }

        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public IReadOnlyCollection<Question> Questions
        {
            get
            {
                if (ProjectId == Guid.Empty) ProjectId = Guid.NewGuid();

                questions.ProjectId = ProjectId;
                return questions;
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