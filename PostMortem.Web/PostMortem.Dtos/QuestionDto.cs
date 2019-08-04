namespace PostMortem.Dtos
{
    using System;
    using System.Collections.Generic;

    public class QuestionDto
    {
        
        public Guid QuestionId { get; set; }
        public Guid ProjectId { get; set; }
        public string QuestionText { get; set; }
        public int ResponseCount { get; set; }
        public int Importance { get; set; }
        public ICollection<CommentDto> Comments { get; set; } = new HashSet<CommentDto>();
    }

}