namespace PostMortem.Domain.Projects
{
    using System;
    using System.Collections.Generic;
    using ChaosMonkey.Guards;
    using Commands;
    using Microsoft.Extensions.Options;
    using Queries;
    using Questions;
    using Zatoichi.EventSourcing;

    public sealed class Project : Aggregate
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
                if (this.ProjectId == Guid.Empty)
                {
                    this.ProjectId = Guid.NewGuid();
                }

                this.questions.ProjectId = this.ProjectId;
                return this.questions;
            }
        }

        public static ProjectGetAllEvent CreateGetAllEventArgs()
        {
            return new ProjectGetAllEvent();
        }

        public static ProjectGetByIdEvent CreateGetByIdEventArgs(Guid projectId)
        {
            return new ProjectGetByIdEvent(projectId);
        }

        public static ProjectCreatedEvent CreateProjectCreatedEventArgs(Project project)
        {
            return new ProjectCreatedEvent(project);
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

        public override void ClearPendingEvents()
        {
            throw new NotImplementedException();
        }

        public override void ApplyEvents()
        {
            throw new NotImplementedException();
        }

        public override void AddEvents(ICollection<IEvent> events)
        {
            throw new NotImplementedException();
        }

        public override int PendingEventCount { get; }
    }
}