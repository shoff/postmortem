namespace PostMortem.Data.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MongoDB.Bson.Serialization.Attributes;

    public class Question
    {
        [BsonId]
        public Guid QuestionId { get; set; }
        [BsonElement("question_text")]
        public string QuestionText { get; set; }
        [BsonElement("response_count")]
        public int ResponseCount { get; set; }
        [Range(1, 500)]
        public Guid ProjectId { get; set; }
        [Range(1, 10)]
        [BsonElement("importance")]
        public int Importance { get; set; }
        [BsonElement("comments")]
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}