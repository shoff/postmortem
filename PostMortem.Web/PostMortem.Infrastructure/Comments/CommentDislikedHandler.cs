namespace PostMortem.Infrastructure.Comments
{
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Commands;
    using Domain.Questions;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class CommentDislikedHandler : INotificationHandler<DislikeCommentCommand>
    {
        private readonly ILogger<CommentDislikedHandler> logger;
        private readonly IRepository repository;

        public CommentDislikedHandler (
            ILogger<CommentDislikedHandler> logger,
            IRepository repository)
        {
            this.logger = Guard.IsNotNull(logger, nameof(logger));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public async Task Handle(DislikeCommentCommand notification, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(notification, nameof(notification));
            var question = await this.repository.GetQuestionByIdAsync(notification.QuestionId, cancellationToken);
            if (question == null)
            {
                throw new QuestionNotFoundException();
            }

            this.logger.LogInformation(notification.Description);
            question.VoteOnComment(notification.CommentId, notification.VoterId, false);
            await this.repository.UpdateQuestionAsync(question, cancellationToken).ConfigureAwait(false);
            question.ClearPendingEvents();
        }
    }
}