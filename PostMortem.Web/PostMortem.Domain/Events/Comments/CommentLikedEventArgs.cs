namespace PostMortem.Domain.Events.Comments
{
    using System;
    using ChaosMonkey.Guards;
    using Domain.Comments;
    using MediatR;
    using Polly;

    public class CommentLikedEventArgs : ExpressionEventBase<Comment>, IRequest<PolicyResult>
    {
        public CommentLikedEventArgs() { }

        public CommentLikedEventArgs(Guid commentId)
        {
            this.CommentId = commentId;
        }

        public Guid CommentId { get; private set; }

        public sealed override string Expression { get; protected set; }

        public override Comment Apply(Comment question)
        {
            Guard.IsNotNull(question, nameof(question));
            question.Likes++;
            return question;
        }
    }
}