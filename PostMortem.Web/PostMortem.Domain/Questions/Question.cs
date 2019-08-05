namespace PostMortem.Domain.Questions
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Commands;
    using Comments;
    using Comments.Commands;
    using Comments.Queries;
    using MediatR;
    using Projects;
    using Zatoichi.EventSourcing;

    public sealed class Question : Aggregate
    {
        private readonly IMediator mediator;
        private readonly int maximumQuestionLength;
        private readonly CommentCollection comments = new CommentCollection();

        public Question(IMediator mediator)
        {
            this.mediator = mediator;
            // HACK HACK HACK HACK HACKEROOONI
            this.QuestionId = Guid.NewGuid();
            this.ProjectId = Guid.NewGuid();
        }

        public Question(Project project)
        {
            Guard.IsNotNull(project, nameof(project));
            this.Options = project.GetOptions().Value;
            this.maximumQuestionLength = this.Options.QuestionMaximumLength;
            this.ProjectId = project.ProjectId;
        }

        public QuestionUpdatedEvent AddQuestionText(string text)
        {
            Guard.IsLessThan(text?.Length ?? 0, this.maximumQuestionLength, nameof(text));
            this.QuestionText = text;
            return CreateQuestionUpdatedEvent(this);
        }

        public Task<Comment> GetByIdAsync(Guid commentId)
        {
            var query = new GetCommentByIdQuery(this.QuestionId, commentId);
            // TODO domain validation here?
            return this.mediator.Send(query);
        }

        public CommentAddedEvent AddComment(Comment comment)
        {
            this.comments.Add(comment);
            // Validation happens here
            return new CommentAddedEvent(comment);

        }

        public Guid QuestionId { get; set; }
        public Guid ProjectId { get; }
        public string QuestionText { get; private set; } = string.Empty;
        public int ResponseCount => this.comments.Count;
        public int Importance { get; set; } 
        public QuestionOptions Options { get; internal set; }
        public IReadOnlyCollection<Comment> Comments
        {
            get
            {
                if (this.QuestionId == Guid.Empty)
                {
                    this.QuestionId = Guid.NewGuid();
                }

                this.comments.QuestionId = this.QuestionId;
                return this.comments;
            }
        }
        public static QuestionAddedEvent CreateQuestionAddedEvent(Question question)
        {
            QuestionAddedEvent questionAddedEvent = new QuestionAddedEvent(question);
            return questionAddedEvent;
        }
        public static QuestionDeletedEventArgs CreateQuestionDeletedEvent(Question question)
        {
            var eventArgs = new QuestionDeletedEventArgs(question);
            return eventArgs;
        }
        private static QuestionUpdatedEvent CreateQuestionUpdatedEvent(Question question)
        {
            var eventArgs = new QuestionUpdatedEvent(question);
            return eventArgs;
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