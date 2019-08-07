namespace PostMortem.Domain.Projects
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Questions;
    using Questions.Events;
    using Zatoichi.Common.Infrastructure.Extensions;
    using Zatoichi.EventSourcing;

    public sealed class Project : Aggregate
    {
        private readonly object syncRoot = new object();
        [JsonProperty]
        private readonly QuestionCollection questions = new QuestionCollection();

        public Project()
        {
        }

        [JsonConstructor]
        public Project(
            string projectName,
            string createdBy,
            DateTime startDate, 
            DateTime? endDate, 
            Guid? id = null)
        {
            this.ProjectId = new ProjectId(id ?? Guid.NewGuid());
            this.ProjectName = projectName;
            this.CreatedBy = createdBy;
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
        public string CreatedBy { get; private set; }
        [JsonProperty]
        public string UpdatedBy { get; private set; }
        [JsonProperty]
        public IReadOnlyCollection<Question> Questions => this.questions;

        public void AddQuestion(string questionText, string author)
        {
            var question = new Question(questionText, this.ProjectId.Id, author);
            var domainEvent = new QuestionAdded(this.ProjectId.Id, question.QuestionId.Id, questionText, author);

            lock (this.syncRoot)
            {
                this.domainEvents.Enqueue(domainEvent);
            }

            this.QuestionAddedEvent.Raise(this, domainEvent);
        }

        public void AddQuestions(ICollection<Question> collection)
        {
            this.questions.AddRange(collection);
        }

        public override void ClearPendingEvents()
        {
            lock (this.syncRoot)
            {
                this.domainEvents.Clear();
            }
        }

        public event EventHandler<QuestionAdded> QuestionAddedEvent;
    }
}