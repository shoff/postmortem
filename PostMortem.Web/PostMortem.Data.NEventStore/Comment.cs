namespace PostMortem.Data.NEventStore
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public Guid CommentId { get; set; }
        
        [Required]
        [StringLength(2000)]
        public string CommentText { get; set; }

        public bool GenerallyPositive { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateAdded { get; set; }

        [Required]
        [StringLength(200)]
        public string Commenter { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public Guid QuestionId { get; set; }
    }
}