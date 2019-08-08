namespace PostMortem.Data.MongoDb
{
    using System;
    using MongoDB.Bson.Serialization.Attributes;

    [BsonIgnoreExtraElements]
    public class EsEvent
    {
        public string VoterId { get; set; }
        public Guid? CommentId { get; set; }
        public Guid? QuestionId { get; set; }
        public Guid? ProjectId { get; set; }
        public string EventType { get; set; }
        public string Body { get; set; }
        public DateTime CommitDate { get; set; }
    }
}