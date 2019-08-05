namespace PostMortem.Domain.Comments.Commands
{
    using Comments;
    using MediatR;
    using Polly;

    public class CommentCommandUpdatedEvent : CommentCommandEvent, IRequest<PolicyResult>
    {
        public CommentCommandUpdatedEvent(Comment comment)
            : base(comment)
        {
        }

        public override void Apply()
        {
            throw new System.NotImplementedException();
        }
    }
}