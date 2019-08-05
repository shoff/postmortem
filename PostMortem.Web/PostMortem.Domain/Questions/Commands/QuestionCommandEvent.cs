namespace PostMortem.Domain.Questions.Commands
{
    using System;
    using ChaosMonkey.Guards;
    using Comments;
    using MediatR;

    public abstract class QuestionCommandEvent : DomainEvent, INotification
    {
        protected QuestionCommandEvent(Comment comment)
        {
            this.Comment = Guard.IsNotNull(comment, nameof(comment));
        }

        public virtual DateTime CreatedDate => DateTime.UtcNow;
        public virtual Comment Comment { get; private set; }
    }
}