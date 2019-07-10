namespace PostMortem.Web.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Comments;
    using Domain.Questions;
    using Zatoichi.Common.Infrastructure.Extensions;

    public class QuestionDto
    {
        
        public Guid QuestionId { get; set; }
        public Guid ProjectId { get; set; }
        public string QuestionText { get; set; }
        public int ResponseCount { get; set; }
        public int Importance { get; set; }
        public ICollection<CommentDto> Comments { get; set; } = new HashSet<CommentDto>();

        public static implicit operator Question(QuestionDto dto)
        {
            List<Comment> comments = dto.Comments.Map(c => (Comment)c).ToList();
            var question = new Question(comments)
            {
                Importance = dto.Importance,
                ProjectId = dto.ProjectId,
                QuestionId = dto.QuestionId,
                QuestionText = dto.QuestionText,
                ResponseCount = dto.ResponseCount
            };
            return question;
        }
    }

}