using PostMortem.Domain.EventSourcing.Events;

namespace PostMortem.Domain.Projects
{
    using System;
    using System.Collections.Generic;
    using ChaosMonkey.Guards;
    
    using Questions;

    public class Project : IEntity<ProjectId>
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

        public ProjectId ProjectId { get; set; } = ProjectId.Empty;
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public IReadOnlyCollection<Question> Questions
        {
            get
            {
                if (ProjectId.Equals(ProjectId.Empty))
                {
                    ProjectId = ProjectId.NewProjectId();
                }

                this.questions.ProjectId = ProjectId;
                return this.questions;
            }
        }

        public void AttachQuestions(IEnumerable<Question> questions)
        {
            this.questions.Clear();
            this.questions.AddRange(questions);
        }
        public ProjectId GetEntityId() => ProjectId;
    }
}