// ReSharper disable InconsistentNaming
namespace PostMortem.Domain.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ChaosMonkey.Guards;
    using Newtonsoft.Json;
    using Questions;
    using Zatoichi.EventSourcing;

    public sealed class Comment : IEntity
    {
        internal const string ANONYMOUS_COWARD = "anonymous";
        private readonly int maxCommentTextLength;
        private readonly int maximumDisLikesPerCommentPerVoter;
        private readonly int maximumLikesPerCommentPerVoter;

        private readonly HashSet<Disposition> dispositions = new HashSet<Disposition>();

        public Comment(
            int maximumDisLikesPerCommentPerVoter,
            int maximumLikesPerCommentPerVoter,
            int maxCommentTextLength,
            string commentText,
            string commenter,
            IQuestionId questionId,
            ICommentId parentId = null,
            ICommentId commentId = null)
        {
            this.maximumDisLikesPerCommentPerVoter = maximumDisLikesPerCommentPerVoter;
            this.maximumLikesPerCommentPerVoter = maximumLikesPerCommentPerVoter;
            this.maxCommentTextLength = maxCommentTextLength;
            this.CommentText = commentText ?? string.Empty;
            this.QuestionId = questionId.Id;
            this.ParentId = parentId;
            this.CommentId = commentId ?? new CommentId(Guid.NewGuid());
            this.Commenter = commenter ?? ANONYMOUS_COWARD;
            this.DateAdded = DateTime.UtcNow;
        }

        public void Vote(Disposition disposition)
        {
            Guard.IsNotNull(disposition, nameof(disposition));

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
        }

        internal void UpdateCommentText(string text)
        {
            if (!string.IsNullOrWhiteSpace(text) && text.Length > this.maxCommentTextLength)
            {
                text = text.Substring(0, this.maxCommentTextLength);
            }
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