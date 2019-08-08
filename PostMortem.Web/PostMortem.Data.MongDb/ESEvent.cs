namespace PostMortem.Data.MongoDb
{
    using System;

    public class EsEvent
    {
        public string VoterId { get; set; }
        public Guid? CommentId { get; set; }
        public Guid? QuestionId { get; set; }
        public Guid? ProjectId { get; set; }
        public Type EventType { get; set; }
        public string Body { get; set; }
        public DateTime CommitDate { get; set; }
    }
}