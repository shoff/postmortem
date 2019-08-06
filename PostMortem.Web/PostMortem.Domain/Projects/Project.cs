namespace PostMortem.Domain.Projects
{
    using System;
    using System.Collections.Generic;
    using ChaosMonkey.Guards;
    using Commands;
    using MediatR;
    using Microsoft.Extensions.Options;
    using Queries;
    using Questions;
    using Zatoichi.EventSourcing;

    public sealed class Project : Aggregate
    {

        private readonly List<INotification> domainEvents;
        private readonly QuestionCollection questions = new QuestionCollection();

        public Project()
        {
            this.ProjectId = Guid.NewGuid();
        }

        public Project(string projectName, DateTime startDate, DateTime? endDate, Guid? id)
        {
            this.ProjectId = id ?? Guid.NewGuid();
            this.ProjectName = projectName;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public Guid ProjectId { get; private set; }
        public string ProjectName { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

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
        
    }
}