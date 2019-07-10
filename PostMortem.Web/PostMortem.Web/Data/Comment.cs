namespace PostMortem.Web.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using MongoDB.Bson.Serialization.Attributes;

    public class Comment
    {
        [BsonId]
        [Range(0, 50000)]
        public Guid CommentId { get; set; }
        
        [Required]
        [StringLength(2000)]
        [BsonElement("comment_text")]
        public string CommentText { get; set; }

        [BsonElement("generally_positive")]
        public bool GenerallyPositive { get; set; }

        [DataType(DataType.DateTime)]
        [BsonElement("date_added")]
        public DateTime DateAdded { get; set; }

        [BsonRequired]
        [BsonElement("commenter")]
        public string Commenter { get; set; }

        [BsonElement("likes")]
        public int Likes { get; set; }

        [BsonElement("dislikes")]
        public int Dislikes { get; set; }

        [Range(0, 500)]
        [BsonElement("question_id")]
        public Guid QuestionId { get; set; }
    }
}