namespace PostMortem.Domain.Comments.Commands
{
    using System;
    using ChaosMonkey.Guards;
    using Comments;
    using Zatoichi.EventSourcing;
    using Zatoichi.EventSourcing.Commands;

    public abstract class CommentCommand : Event, ICommand
    {
        protected CommentCommand(Comment comment)
        {
            this.Comment = Guard.IsNotNull(comment, nameof(comment));
        }

        public virtual DateTime CreatedDate => DateTime.UtcNow;
        public virtual Comment Comment { get; private set; }
        public string Description { get; set; }
    }
}