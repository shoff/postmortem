namespace PostMortem.Domain.Projects
{
    using System;
    using System.Collections.Generic;
    using ChaosMonkey.Guards;
    using Newtonsoft.Json;
    using Questions;
    using Zatoichi.EventSourcing;

    public sealed class Project : Aggregate
    {
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
        public IReadOnlyCollection<Question> Questions
        {
            get
            {
                this.questions.ProjectId = this.ProjectId.Id;
                return this.questions;
            }
        }

        public void AddQuestions(ICollection<Question> collection)
        {
            Guard.IsNotNull(collection, nameof(collection));
            this.questions.AddRange(collection);
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