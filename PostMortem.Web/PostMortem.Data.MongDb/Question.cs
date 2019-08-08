namespace PostMortem.Data.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MongoDB.Bson.Serialization.Attributes;

    [BsonIgnoreExtraElements]
    public class Question
    {
        [BsonElement("question_id")]
        public Guid QuestionId { get; set; }

        [BsonElement("question_text")]
        public string QuestionText { get; set; }

        [BsonElement("response_count")]
        public int ResponseCount { get; set; }

        [BsonRequired]
        public Guid ProjectId { get; set; }

        [Range(1, 10)]
        [BsonElement("importance")]
        public int Importance { get; set; }

        [BsonElement("author")]
        public string Author { get; set; }

        [BsonElement("commit_date")]
        public DateTime CommitDate { get; set; } = DateTime.UtcNow;

        [BsonElement("comments")]
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}