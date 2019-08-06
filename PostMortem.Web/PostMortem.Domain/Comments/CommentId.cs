namespace PostMortem.Domain.Comments
{
    using System;
    using Newtonsoft.Json;

    public class CommentId : ICommentId
    {
        public CommentId(Guid id)
        {
            this.Id = id;
        }

        [JsonProperty]
        public Guid Id { get; private set; }
    }
}