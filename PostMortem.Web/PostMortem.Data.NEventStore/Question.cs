namespace PostMortem.Data.NEventStore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public class Question
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int ResponseCount { get; set; }
        public Guid ProjectId { get; set; }
        [Range(1, 10)]
        public int Importance { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}