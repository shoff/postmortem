namespace PostMortem.Domain.Projects
{
    using System;
    using System.Collections.Generic;
    using ChaosMonkey.Guards;
    using Events;
    using Microsoft.Extensions.Options;
    using Questions;

    public class Project
    {
        private readonly QuestionCollection questions = new QuestionCollection();

        public Project()
        {
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

        public void AddQuestions(ICollection<Question> collection)
        {
            Guard.IsNotNull(collection, nameof(collection));
            this.questions.AddRange(collection);
        }
        public IOptions<QuestionOptions> GetOptions()
        {
            throw new NotImplementedException();
        }
    }
}