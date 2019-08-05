namespace PostMortem.Domain.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ChaosMonkey.Guards;
    using Commands;
    using Questions;
    using Zatoichi.EventSourcing;

    public sealed class Comment : EventEntity
    {
        private Guid commentId = Guid.Empty;
        private readonly int maxCommentTextLength;
        private readonly int maximumDisLikesPerCommentPerVoter;
        private readonly int maximumLikesPerCommentPerVoter;
        private readonly HashSet<Disposition> dispositions = new HashSet<Disposition>();

        public Comment(
            Question question)
        {
            Guard.IsNotNull(question, nameof(question));
            this.maximumDisLikesPerCommentPerVoter = question.Options.MaximumDisLikesPerCommentPerVoter;
            this.maximumLikesPerCommentPerVoter = question.Options.MaximumLikesPerCommentPerVoter;
            this.maxCommentTextLength = question.Options.CommentMaximumLength;
            this.QuestionId = question.QuestionId;
            this.DateAdded = DateTime.UtcNow;
        }

        public Guid CommentId
        {
            get
            {
                if (this.commentId == Guid.Empty)
                {
                    this.commentId = Guid.NewGuid();
                }

                return this.commentId;
            }
            set => this.commentId = value;
        }

        public CommentCommandEvent Vote(Disposition disposition)
        {
            Guard.IsNotNull(disposition, nameof(disposition));

            //var count = (from c in this.dispositions
            //    where c.VoterId.Id == disposition.VoterId.Id && c.Liked == disposition.Liked
            //    select c).ToArray().Length;

            
            var count = this.dispositions.Count(v => v.VoterId.Id == disposition.VoterId.Id && (bool)v == (bool)disposition);


            if (!disposition)
            {
                if (count < this.maximumDisLikesPerCommentPerVoter)
                {
                    this.dispositions.Add(disposition);
                }
            }
            else
            {
                if (count < this.maximumLikesPerCommentPerVoter)
                {
                    this.dispositions.Add(disposition);
                }
            }
            // probably shouldn't publish an event if nothing here has been added...
            if (disposition.Liked)
            {
                return new CommentLikedEvent(this);
            }

            return new CommentDislikedEvent(this);

        }

        public Guid QuestionId { get; private set; }

        public string CommentText { get; private set; } = string.Empty;

        public bool GenerallyPositive => this.Likes > this.Dislikes;

        public DateTime DateAdded { get; private set; }

        public string Commenter { get; private set; } = string.Empty;

        public int Likes => this.dispositions.Sum(d => d.Liked ? 1 : 0);

        public int Dislikes => this.dispositions.Sum(d => d.Liked ? 0 : 1);

        public CommentCommandAddedEvent AddCommentText(string text)
        {
            Guard.IsLessThan(text?.Length ?? 0, this.maxCommentTextLength, nameof(text));
            this.CommentText = $"{this.CommentText} {text}";
            return CreateCommentAddedEvent(this);
        }
        public CommentCommandReplacedEvent ReplaceCommentText(string text)
        {
            Guard.IsLessThan(text?.Length ?? 0, this.maxCommentTextLength, nameof(text));
            this.CommentText = text;
            return CreateCommentReplacedEvent(this);
        }

        private static CommentCommandReplacedEvent CreateCommentReplacedEvent(Comment comment)
        {
            var eventArgs = new CommentCommandReplacedEvent(comment);
            return eventArgs;
        }
        private static CommentCommandAddedEvent CreateCommentAddedEvent(Comment comment)
        {
            var eventArgs = new CommentCommandAddedEvent(comment);
            return eventArgs;
        }
        private static CommentLikedEvent CreateCommentLikedEvent(Comment comment)
        {
            var eventArgs = new CommentLikedEvent(comment);
            return eventArgs;
        }
        private static CommentDislikedEvent CreateCommentDislikedEvent(Comment comment)
        {
            var eventArgs = new CommentDislikedEvent(comment);
            return eventArgs;
        }
        private static CommentCommandUpdatedEvent CreateCommentUpdatedEvent(Comment comment)
        {
            var eventArgs = new CommentCommandUpdatedEvent(comment);
            return eventArgs;
        }
    }
}