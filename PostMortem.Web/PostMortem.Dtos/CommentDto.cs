namespace PostMortem.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CommentDto
    {
        public CommentDto(Guid? commentId = null)
        {
            // TODO need to figure out the best way to do this
            if (commentId.HasValue)
            {
                this.CommentId = (Guid)commentId;
            }
            else
            {
                this.CommentId = Guid.Empty;
            }
        }
        public Guid CommentId { get; set; }
        public Guid? ParentId { get; set; }

        [Required]
        public Guid QuestionId { get; set; }

        [Required]
        [StringLength(1024)]
        public string CommentText { get; set; }

        // TODO I will add Machine Sentiment Learning to this property for my ML 80
        public bool GenerallyPositive { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

        [StringLength(80)]
        public string Commenter { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
    }
}