namespace PostMortem.Domain.Questions
{
    using System;
    using System.Collections.Generic;
    using ChaosMonkey.Guards;
    using Comments;


    public class Question
    {
        private readonly CommentCollection comments = new CommentCollection();
        
        public Question() { }

        public Question(ICollection<Comment> comments)
        {
            Guard.IsNotNull(comments, nameof(comments));
            this.comments.AddRange(comments);
        }

        public Guid QuestionId { get; set; }
        public Guid ProjectId { get; set; }
        public string QuestionText { get; set; }
        public int ResponseCount { get; set; }
        public int Importance { get; set; }

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
    }
}