namespace PostMortem.Domain.Projects
{
    using System;
    using System.Collections.Generic;
    using ChaosMonkey.Guards;
    using Commands;
    using MediatR;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Queries;
    using Questions;
    using Zatoichi.EventSourcing;

    public sealed class Project : Aggregate
    {

        private readonly List<INotification> domainEvents;
        private readonly QuestionCollection questions = new QuestionCollection();

        [JsonConstructor]
        public Project()
        {
        }

        public Project(string projectName, DateTime startDate, DateTime? endDate, Guid? id)
        {
            this.ProjectId = new ProjectId(id ?? Guid.NewGuid());
            this.ProjectName = projectName;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        [JsonProperty]
        public IProjectId ProjectId { get; private set; }
        [JsonProperty]
        public string ProjectName { get; private set; }
        [JsonProperty]
        public DateTime StartDate { get; private set; }
        [JsonProperty]
        public DateTime? EndDate { get; private set; }

        [JsonProperty]
        public IReadOnlyCollection<Question> Questions
        {
            get
            {
                this.questions.ProjectId = this.ProjectId.Id;
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