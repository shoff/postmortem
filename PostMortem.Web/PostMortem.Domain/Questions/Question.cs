namespace PostMortem.Domain.Questions
{
    using System;
    using System.Collections.Generic;
    using Comments;


    public class Question
    {
        private readonly CommentCollection comments = new CommentCollection();
        
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