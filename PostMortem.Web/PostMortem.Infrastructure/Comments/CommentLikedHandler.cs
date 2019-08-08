namespace PostMortem.Infrastructure.Comments
{
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Commands;
    using Domain.Questions;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class CommentLikedHandler : INotificationHandler<LikeCommentCommand>
    {
        private readonly ILogger<CommentLikedHandler> logger;
        private readonly IRepository repository;

        public CommentLikedHandler(
            ILogger<CommentLikedHandler> logger,
            IRepository repository)
        {
            this.logger = Guard.IsNotNull(logger, nameof(logger));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public async Task Handle(LikeCommentCommand notification, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(notification, nameof(notification));
            var question = await this.repository.GetQuestionByIdAsync(notification.QuestionId, cancellationToken);
            if (question == null)
            {
                throw new QuestionNotFoundException();
            }

            this.logger.LogInformation(notification.Description);
            question.VoteOnComment(notification.CommentId, notification.VoterId, true);
            await this.repository.UpdateQuestionAsync(question, cancellationToken);
        }
    }
}