namespace PostMortem.Infrastructure.Comments
{
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Commands;
    using MediatR;

    public class CommentUpdatedHandler : INotificationHandler<AddCommentCommand>
    {
        private readonly IRepository repository;

        public CommentUpdatedHandler(
            IRepository repository)
        {
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public Task Handle(AddCommentCommand notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}