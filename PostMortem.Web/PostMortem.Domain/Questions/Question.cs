namespace PostMortem.Domain.Questions
{
    using System;
    using System.Collections.Generic;
    using Comments;
    using Comments.Events;
    using Events;
    using Newtonsoft.Json;
    using Projects;
    using Zatoichi.EventSourcing;

    public sealed class Question : Aggregate
    {
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
            this.ProjectId = projectId.Id;
            this.QuestionId = questionId ?? new QuestionId(Guid.NewGuid());
            this.QuestionText = questionText;
        }

        public void AddQuestionText(string text)
        {
            this.QuestionText = text;
            this.AddDomainEvent(new QuestionTextUpdated(this.QuestionId, text));
        }

        public void AddComment(string commentText, string commenter, Guid? parentId = null)
        {
            var comment = new Comment(
                this.Options.MaximumDisLikesPerCommentPerVoter,
                this.Options.MaximumLikesPerCommentPerVoter,
                this.Options.QuestionMaximumLength,
                commentText,
                commenter,
                this.QuestionId,
                new CommentId(Guid.NewGuid()))
            {
                ParentId = parentId != null ? new CommentId((Guid) parentId) : null
            };
            
            this.comments.Add(comment); // pseudo snapshot :)

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
            // TODO this can be used as sort of "commit transaction" 
        }

        public override void ApplyEvents()
        {
            // only here to make debugging easier
            base.ApplyEvents();
        }
    }
}