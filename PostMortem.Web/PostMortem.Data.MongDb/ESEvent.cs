namespace PostMortem.Data.MongoDb
{
    using System;

    public class ESEvent
    {
        public Type EventType { get; set; }
        public string Body { get; set; }
        public DateTime CommitDate { get; set; }
    }
}