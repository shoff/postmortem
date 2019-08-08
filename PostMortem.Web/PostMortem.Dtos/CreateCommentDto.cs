namespace PostMortem.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateCommentDto
    {

        [Required]
        public Guid QuestionId { get; set; }
        public Guid? ParentId { get; set; }
        [Required]
        [StringLength(1024)]
        public string CommentText { get; set; }
    }
}