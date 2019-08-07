namespace PostMortem.Domain.Questions
{
    using System;
    using System.Collections.Generic;
    using Comments;
    using Comments.Events;
    using Events;
    using Newtonsoft.Json;
    using Zatoichi.Common.Infrastructure.Extensions;
    using Zatoichi.EventSourcing;

    public sealed class Question : Aggregate
    {
        private readonly object syncRoot = new object();

        [JsonProperty]
        private readonly CommentCollection comments = new CommentCollection();
        public event EventHandler<QuestionTextUpdated> QuestionTextUpdatedEvent;

        public Question() { }

        [JsonConstructor]
        public Question(
            string questionText,
            QuestionOptions questionOptions,
            Guid projectId,
            Guid? questionId = null)
        {
            this.Options = questionOptions;
            this.ProjectId = projectId;
            this.QuestionId = new QuestionId(questionId ?? Guid.NewGuid());
            this.QuestionText = questionText;
        }

        public void AddQuestionText(string text)
        {
            this.QuestionText = text;
            var domainEvent = new QuestionTextUpdated(this.QuestionId.Id, text);
            this.AddDomainEvent(domainEvent);
            this.QuestionTextUpdatedEvent.Raise(this, domainEvent);
        }

        public void AddComment(string commentText, string commenter, Guid? commentId = null, Guid? parentId = null)
        {
            var comment = new Comment(
                this.Options.MaximumDisLikesPerCommentPerVoter,
                this.Options.MaximumLikesPerCommentPerVoter,
                this.Options.QuestionMaximumLength,
                commentText,
                commenter,
                this.QuestionId,
                new CommentId(commentId ?? Guid.NewGuid()))
            {
                ParentId = parentId != null ? new CommentId((Guid)parentId) : null
            };

            this.comments.Add(comment); // pseudo snapshot :)

            lock (this.syncRoot)
            {
                this.domainEvents.Enqueue(
                    new CommentAdded(
                        this.Options.MaximumDisLikesPerCommentPerVoter,
                        this.Options.MaximumLikesPerCommentPerVoter,
                        this.Options.QuestionMaximumLength,
                        commentText,
                        commenter,
                        this.QuestionId.Id,
                        comment.CommentId.Id,
                        parentId));
            }
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
        public override void ClearPendingEvents()
        {
            lock (this.syncRoot)
            {
                this.domainEvents.Clear();
            }
        }
    }
}