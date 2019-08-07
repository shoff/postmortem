// ReSharper disable InconsistentNaming
namespace PostMortem.Domain.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using Questions;
    using Zatoichi.EventSourcing;

    public sealed class Comment : IEntity
    {
        private readonly HashSet<Disposition> dispositions = new HashSet<Disposition>();

        public Comment(

            string commentText,
            string commenter,
            IQuestionId questionId,
            ICommentId parentId = null,
            ICommentId commentId = null)
        {
            this.CommentText = commentText ?? string.Empty;
            this.QuestionId = questionId.Id;
            this.ParentId = parentId;
            this.CommentId = commentId ?? new CommentId(Guid.NewGuid());
            this.Commenter = commenter ?? Constants.ANONYMOUS_COWARD;
            this.DateAdded = DateTime.UtcNow;
        }

        public void Vote(Disposition disposition)
        {
            this.dispositions.Add(disposition);
        }

        internal void UpdateCommentText(string text)
        {
            this.CommentText = text ?? string.Empty;
        }
        [JsonProperty]
        public int Order { get; internal set; }
        [JsonProperty]
        public ICommentId ParentId { get; internal set; }
        [JsonProperty]
        public ICommentId CommentId { get; private set; }
        [JsonProperty]
        public Guid QuestionId { get; private set; }
        [JsonProperty]
        public string CommentText { get; private set; }
        public bool GenerallyPositive => this.Likes > this.Dislikes;
        [JsonProperty]
        public DateTime DateAdded { get; private set; }
        [JsonProperty]
        public string Commenter { get; private set; }
        public int Likes => this.dispositions.Sum(d => d.Liked ? 1 : 0);
        public int Dislikes => this.dispositions.Sum(d => d.Liked ? 0 : 1);
    }
}