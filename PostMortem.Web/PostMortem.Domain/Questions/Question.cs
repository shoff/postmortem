using System.Threading.Tasks;
using Polly;

namespace PostMortem.Domain.Questions
{
    using System;
    using System.Collections.Generic;
    using ChaosMonkey.Guards;
    using Comments;


    public class Question : IEntity<QuestionId>
    {
        private readonly CommentCollection comments = new CommentCollection();

        public Question()
            : this(new List<Comment>()) { }

        public Question(Guid questionId)
        {
            QuestionId = new QuestionId(questionId);
        }

        public Question(ICollection<Comment> comments)
        {
            Guard.IsNotNull(comments, nameof(comments));
            this.QuestionId = QuestionId;
            this.comments.AddRange(comments);
        }

        public QuestionId QuestionId { get; set; } = QuestionId.Empty;
        public Guid ProjectId { get; set; }
        public string QuestionText { get; set; }
        public int ResponseCount { get; set; }
        public int Importance { get; set; }
        public bool Active { get; set; } = true;
        public DateTime? LastUpdated { get; set; }

        public IReadOnlyCollection<Comment> Comments
        {
            get
            {
                if (this.QuestionId.Equals(QuestionId.Empty))
                {
                    this.QuestionId = QuestionId.NewQuestionId();
                }

                this.comments.QuestionId = this.QuestionId;
                return this.comments;
            }
        }

        public void AttachComments(IEnumerable<Comment> comments)
        {
            this.comments.Clear();
            this.comments.AddRange(comments);
        }

        public QuestionId GetEntityId() => QuestionId;
    }
}