namespace PostMortem.Web.Dtos
{
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class NotFoundResult : ReplaceOneResult
    {
        public NotFoundResult(
            bool isAcknowledged = true, 
            bool isModifiedCountAvailable = false, 
            long matchedCount = 0,
            long modifiedCount = 0, 
            BsonValue id = null)
        {
            this.IsAcknowledged = isAcknowledged;
            this.IsModifiedCountAvailable = isModifiedCountAvailable;
            this.MatchedCount = matchedCount;
            this.ModifiedCount = modifiedCount;
            this.UpsertedId = id;
        }

        public static NotFoundResult Default => new NotFoundResult();

        public override bool IsAcknowledged { get; }
        public override bool IsModifiedCountAvailable { get; }
        public override long MatchedCount { get; }
        public override long ModifiedCount { get; }
        public override BsonValue UpsertedId { get; }
    }
}