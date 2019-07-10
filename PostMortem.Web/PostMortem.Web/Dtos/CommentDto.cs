namespace PostMortem.Web.Dtos
{
    using System;
    using Domain.Comments;

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

        public static implicit operator Comment(CommentDto dto)
        {
            var comment = new Comment
            {
                Commenter = dto.Commenter,
                CommentId = dto.CommentId,
                CommentText = dto.CommentText,
                DateAdded = dto.DateAdded,
                Dislikes = dto.Dislikes,
                GenerallyPositive = dto.GenerallyPositive,
                Likes = dto.Likes,
                QuestionId = dto.QuestionId
            };
            return comment;
        }
    }
}