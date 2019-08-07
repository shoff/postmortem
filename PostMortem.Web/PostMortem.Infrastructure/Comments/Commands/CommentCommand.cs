namespace PostMortem.Infrastructure.Comments.Commands
{
    using System;
    using ChaosMonkey.Guards;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing.Commands;

    public abstract class CommentCommand : ICommand
    {
        protected CommentCommand(Guid commentId)
        {
            this.CommentId = Guard.IsNotDefault(commentId, nameof(commentId));
        }

        [JsonProperty]
        public virtual DateTime CreatedDate { get; private set; } = DateTime.UtcNow;

        [JsonProperty]
        public Guid CommentId { get; private set; }

        public string Description { get; set; } = "";
    }
}