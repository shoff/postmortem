namespace PostMortem.Dtos
{
    using System;

    public class QuestionDto
    {
        private readonly CommentCollection comments = new CommentCollection();
        
        public Guid QuestionId { get; set; }
        public Guid ProjectId { get; set; }
        public string QuestionText { get; set; }
        public int ResponseCount { get; set; }
        public int Importance { get; set; }

        public CommentCollection Comments
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