namespace PostMortem.Domain.Comments.Commands
{
    using System;
    using ChaosMonkey.Guards;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing;
    using Zatoichi.EventSourcing.Commands;

    public abstract class CommentCommand : Event, ICommand
    {
        protected CommentCommand(Guid commentId)
        {
            this.CommentId = Guard.IsNotDefault(commentId, nameof(commentId));
            this.CreatedDate = DateTime.UtcNow;
        }

        [JsonProperty]
        public virtual DateTime CreatedDate { get; private set; }
        [JsonProperty]
        public Guid CommentId { get; private set; }
        public string Description { get; set; }
    }
}