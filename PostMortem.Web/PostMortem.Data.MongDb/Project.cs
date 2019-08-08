namespace PostMortem.Data.MongoDb
{
    using System;
    using MongoDB.Bson.Serialization.Attributes;

    public class Project
    {
        [BsonId]
        public Guid ProjectId { get; set; }

        [BsonElement("project_name")]
        public string ProjectName { get; set; }

        [BsonElement("start_date")]
        public DateTime StartDate { get; set; }

        [BsonElement("end_date")]
        public DateTime? EndDate { get; set; }

        [BsonElement("created_by")]
        public string CreatedBy { get; set; }

        [BsonElement("commit_date")]
        public DateTime CommitDate { get; set; } = DateTime.UtcNow;

    }
}