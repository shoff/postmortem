namespace PostMortem.Domain.Questions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Comments;
    using Comments.Events;
    using Events;
    using Newtonsoft.Json;
    using Voters;
    using Zatoichi.Common.Infrastructure.Extensions;
    using Zatoichi.EventSourcing;

    public sealed class Question : Aggregate
    {
        private readonly object syncRoot = new object();

        [JsonProperty]
        private readonly CommentCollection comments = new CommentCollection();
        public event EventHandler<QuestionUpdated> QuestionTextUpdatedEvent;

        public Question() { }

        [JsonConstructor]
        public Question(
            string questionText,
            Guid projectId,
            string author,
            Guid? questionId = null)
        {
            this.Author = author;
            this.ProjectId = projectId;
            this.QuestionId = new QuestionId(questionId ?? Guid.NewGuid());
            this.QuestionText = questionText;
        }

        public void Update(string text, string author)
        {
            this.QuestionText = text;
            var domainEvent = new QuestionUpdated(this.QuestionId.Id, this.ProjectId, text, author);
            this.AddDomainEvent(domainEvent);
            this.QuestionTextUpdatedEvent.Raise(this, domainEvent);
        }

        public void AddComment(string commentText, string commenter, Guid? commentId = null, Guid? parentId = null)
        {
            var comment = new Comment(
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
                        commentText,
                        commenter,
                        this.QuestionId.Id,
                        comment.CommentId.Id,
                        parentId));
            }
        }

        public void VoteOnComment(Guid commentId, string author, bool liked)
        {
            var comment = (from c in this.comments
                where c.CommentId.Id == commentId
                select c).FirstOrDefault();

            if (comment == null)
            {
                throw new CommentNotFoundException();
            }

            var events = this.Build(comment.CommentId.Id, author, liked);
            comment.Vote(events.disposition);

            lock (this.syncRoot)
            {
                this.domainEvents.Enqueue(events.domainEvent);
            }
            
        }

        [JsonProperty]
        public IQuestionId QuestionId { get; private set; }
        [JsonProperty]
        public Guid ProjectId { get; private set; }
        [JsonProperty]
        public string QuestionText { get; private set; } = string.Empty;
        [JsonProperty]
        public string Author { get; private set; }
        public int ResponseCount => this.comments.Count;
        public int Importance { get; set; }
        public IReadOnlyCollection<Comment> Comments => this.comments;
        public override void ClearPendingEvents()
        {
            lock (this.syncRoot)
            {
                this.domainEvents.Clear();
            }
        }
        private (Disposition disposition, CommentEvent domainEvent) Build(Guid commentId, string author, bool liked)
        {
            var disposition = liked ? 
                new Like(new VoterId(author)) : 
                new DisLike(new VoterId(author)) as Disposition;

            var domainEvent = liked ?
                new CommentLiked(commentId, this.QuestionId.Id, author) :
                new CommentDisliked(commentId, this.QuestionId.Id, author) as CommentEvent;

            return (disposition, domainEvent);
        }
    }
}