using PostMortem.Data.MongoDb;
using PostMortem.Domain.EventSourcing.Events;

namespace PostMortem.Domain.Projects
{
    using System;
    using System.Collections.Generic;
    using ChaosMonkey.Guards;
    using Events.Projects;
    using Questions;

    public class Project : IEntity<ProjectId>
    {
        private readonly QuestionCollection questions = new QuestionCollection();
        IProjectRepository repository;

        public Project()
            : this(new List<Question>(),null)
        {
        }

        public Project(ICollection<Question> questions, IProjectRepository repository)
        {
            Guard.IsNotNull(questions, nameof(questions));
            this.questions.AddRange(questions);
        }

        public ProjectId ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public IReadOnlyCollection<Question> Questions
        {
            get
            {
                if (ProjectId == ProjectId.Empty)
                {
                    ProjectId = ProjectId.NewProjectId();
                }

                this.questions.ProjectId = ProjectId.Id;
                return this.questions;
            }
        }

        public ProjectId GetEntityId() => ProjectId;
    }
}