namespace PostMortem.Domain.Questions
{
    using System;
    using System.Collections.Generic;
    using Commands;
    using Comments;
    using Comments.Commands;
    using Events;
    using MediatR;
    using Newtonsoft.Json;
    using Projects;
    using Zatoichi.EventSourcing;

    public sealed class Question : Aggregate
    {

        // TODO fix this tonight
        private List<INotification> domainNotifications = new List<INotification>();
        private readonly int maximumQuestionLength;
        [JsonProperty]
        private readonly CommentCollection comments = new CommentCollection();

        public Question() { }

        [JsonConstructor]
        public Question(
            string questionText,
            QuestionOptions questionOptions, 
            IProjectId projectId,  
            IQuestionId questionId = null)
        {
            this.Options = questionOptions;
            this.maximumQuestionLength = this.Options.QuestionMaximumLength;
            this.ProjectId = projectId.Id;
            this.QuestionId = questionId ?? new QuestionId(Guid.NewGuid());
            this.QuestionText = questionText;
        }

        public void AddQuestionText(string text)
        {
            this.QuestionText = text;
            this.domainNotifications.Add(new QuestionTextUpdated(this));
        }

        public void AddComment(Comment comment)
        {
            this.comments.Add(comment);
            
            // Validation happens here
            return new AddCommentCommand(comment);
        }

        [JsonProperty]
        public IQuestionId QuestionId { get; private set; }
        [JsonProperty]
        public Guid ProjectId { get; private set; }
        [JsonProperty]
        public string QuestionText { get; private set; } = string.Empty;
        public int ResponseCount => this.comments.Count;
        public int Importance { get; set; } 
        [JsonProperty]
        public QuestionOptions Options { get; internal set; }
        public IReadOnlyCollection<Comment> Comments => this.comments;

        public static AddQuestionCommand CreateQuestionAddedEvent(Question question)
        {
            AddQuestionCommand addQuestionCommand = new AddQuestionCommand(question);
            return addQuestionCommand;
        }

        public static DeleteQuestionCommand CreateQuestionDeletedEvent(Question question)
        {
            var eventArgs = new DeleteQuestionCommand(question);
            return eventArgs;
        }

        private static UpdateQuestionCommand CreateQuestionUpdatedEvent(Question question)
        {
            var eventArgs = new UpdateQuestionCommand(question);
            return eventArgs;
        }

        public override void ClearPendingEvents()
        {
            // TODO this can be used as sort of "commit transaction" 
        }

        public override void ApplyEvents()
        {
            // only here to make debugging easier
            base.ApplyEvents();
        }
    }
}