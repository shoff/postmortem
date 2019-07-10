namespace PostMortem.Dtos
{
    using System;

    public class CommentDto
    {
        public Guid CommentId { get; set; }
        public Guid QuestionId { get; set; }
        public string CommentText { get; set; }
        public bool GenerallyPositive { get; set; }
        public DateTime DateAdded { get; set; }
        public string Commenter { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}