namespace PostMortem.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CommentDto
    {
        private Guid commentId;

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

        [Required]
        public Guid QuestionId { get; set; }
        [StringLength(1024)]
        public string CommentText { get; set; }
        // TODO I will add Machine Sentiment Learning to this property for my ML 80
        public bool GenerallyPositive { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        public string Commenter { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}