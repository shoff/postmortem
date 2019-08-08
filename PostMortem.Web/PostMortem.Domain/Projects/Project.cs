namespace PostMortem.Domain.Projects
{
    using System;
    using System.Collections.Generic;
    using Events;
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

        public void Update(string projectName, string author, DateTime? startDate = null, DateTime? endDate = null)
        {
            this.ProjectName = projectName;
            this.UpdatedBy = string.IsNullOrWhiteSpace(author) ? Constants.ANONYMOUS_COWARD : author;
            this.StartDate = startDate ?? this.StartDate;
            this.EndDate = endDate ?? this.EndDate;
            var domainEvent = new ProjectUpdated(this.ProjectId.Id, this.UpdatedBy, this.StartDate, this.EndDate);
            lock (this.syncRoot)
            {
                this.domainEvents.Enqueue(domainEvent);
            }
        }

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
            // Do not add domain events here. This is used to reconstitute from 
            // the repository
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