namespace PostMortem.Domain.Questions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Comments;
    using Comments.Events;
    using Events;
    using Newtonsoft.Json;
    using Voters;
    using Zatoichi.Common.Infrastructure.Extensions;
    using Zatoichi.EventSourcing;

    public sealed class Question : Aggregate
    {
        private readonly CommentCollection comments = new CommentCollection();
        private readonly object syncRoot = new object();
        public event EventHandler<QuestionUpdated> QuestionTextUpdatedEvent;

        public Question(
            string questionText,
            Guid projectId,
            string author,
            Guid? questionId = null)
            : this(questionText, projectId, author, null, questionId)
        {
        }

        public Question(
            string questionText,
            Guid projectId,
            string author,
            DateTime? lastUpdate,
            Guid? questionId = null)
        {
            this.Author = author;
            this.ProjectId = projectId;
            this.QuestionId = new QuestionId(questionId ?? Guid.NewGuid());
            this.QuestionText = string.IsNullOrWhiteSpace(questionText) ? "Please enter your question" : questionText;
            this.LastUpdated = lastUpdate;
        }

        public IQuestionId QuestionId { get; }
        public Guid ProjectId { get; }
        public string QuestionText { get; private set; }
        public string Author { get; }
        public int ResponseCount => this.comments.Count;
        public DateTime? LastUpdated { get; private set; }
        public int Importance { get; set; }
        public IReadOnlyCollection<Comment> Comments => this.comments;

        public void Update(string text, string author)
        {
            this.QuestionText = text;
            var domainEvent = new QuestionUpdated(this.QuestionId.Id, this.ProjectId, text, author);
            this.AddDomainEvent(domainEvent);
            this.LastUpdated = DateTime.UtcNow;
            this.QuestionTextUpdatedEvent.Raise(this, domainEvent);
        }

        public Guid AddComment(string commentText, string commenter, Guid? commentId = null, Guid? parentId = null)
        {
            var comment = new Comment(
                commentText,
                commenter,
                this.QuestionId,
                new CommentId(commentId ?? Guid.NewGuid()))
            {
                ParentId = parentId != null ? new CommentId((Guid) parentId) : null
            };

            this.comments.Add(comment); // pseudo snapshot :)

            lock (this.syncRoot)
            {
                if (this.domainEvents == null)
                {
                    this.domainEvents = new Queue<DomainEvent>();
                }

                Expression<Action<Comment, CommentCollection>> apply = (c, collection) => collection.Add(c);
                this.domainEvents.Enqueue(
                    new CommentAdded(
                        commentText,
                        commenter,
                        this.QuestionId.Id,
                        comment.CommentId.Id,
                        parentId));
            }

            this.LastUpdated = DateTime.UtcNow;
            return comment.CommentId.Id;
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
                if (this.domainEvents == null)
                {
                    this.domainEvents = new Queue<DomainEvent>();
                }

                this.domainEvents.Enqueue(events.domainEvent);
            }

            this.LastUpdated = DateTime.UtcNow;
        }

        public override void ClearPendingEvents()
        {
            lock (this.syncRoot)
            {
                this.domainEvents?.Clear();
            }
        }

        // Added so we can reconstitute from a snapshot
        public void AddComments(ICollection<Comment> comments)
        {
            if (comments == null)
            {
                // guaranteed valid
                return;
            }

            this.comments.AddRange(comments);
            this.LastUpdated = DateTime.UtcNow;
        }

        public void ApplyEvents(ICollection<DomainEvent> domainEvents)
        {
            // HACK - just to get it done for the demo
            foreach (var @event in domainEvents)
            {
                if (@event is CommentEvent ce)
                {
                    if (this.comments.FirstOrDefault(c => c.CommentId.Id == ce.CommentId) == null)
                    {
                        var comment = new Comment("", "", new QuestionId(ce.QuestionId), null,
                            new CommentId(ce.CommentId));
                        this.comments.Add(comment);
                    }
                }
            }

            this.LastUpdated = DateTime.UtcNow;
        }

        private (Disposition disposition, CommentEvent domainEvent) Build(Guid commentId, string author, bool liked)
        {
            var disposition = liked ? new Like(new VoterId(author)) : new DisLike(new VoterId(author)) as Disposition;

            var domainEvent = liked
                ? new CommentLiked(commentId, this.QuestionId.Id, author)
                : new CommentDisliked(commentId, this.QuestionId.Id, author) as CommentEvent;

            return (disposition, domainEvent);
        }
    }
}