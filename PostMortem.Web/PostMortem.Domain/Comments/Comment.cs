namespace PostMortem.Domain.Comments
{
    using System;
    using ChaosMonkey.Guards;
    using Commands;
    using Questions;
    using Zatoichi.EventSourcing;

    public class Comment : EventEntity
    {
        private Guid commentId = Guid.Empty;
        private readonly int maxCommentTextLength;

        public Comment(
            Question question)
        {
            Guard.IsNotNull(question, nameof(question));

            this.maxCommentTextLength = question.Options.CommentMaximumLength;
            this.QuestionId = question.QuestionId;
            this.DateAdded = DateTime.UtcNow;
            this.Likes = 0;
            this.Dislikes = 0;
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
        public Guid QuestionId { get; private set; }
        public string CommentText { get; private set; } = string.Empty;
        public bool GenerallyPositive { get; private set; } = true;
        public DateTime DateAdded { get; private set; }
        public string Commenter { get; private set; } = string.Empty;
        public int Likes { get; private set; }
        public int Dislikes { get; private set; }

        public CommentCommandAddedEvent AddCommentText(string text)
        {
            Guard.IsLessThan(text?.Length ?? 0, this.maxCommentTextLength, nameof(text));
            this.CommentText = $"{this.CommentText} {text}";
            return CreateCommentAddedEvent(this);
        }
        public CommentCommandReplacedEvent ReplaceCommentText(string text)
        {
            Guard.IsLessThan(text?.Length ?? 0, this.maxCommentTextLength, nameof(text));
            this.CommentText =text;
            return CreateCommentReplacedEvent(this);
        }
        public CommentLikedEvent Like()
        {
            return CreateCommentLikedEvent(this);
        }
        public CommentDislikedEvent Dislike()
        {
            return CreateCommentDislikedEvent(this);
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