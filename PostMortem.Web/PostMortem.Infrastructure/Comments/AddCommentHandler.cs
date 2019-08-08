namespace PostMortem.Infrastructure.Comments
{
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Commands;
    using Domain.Questions;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class AddCommentHandler : INotificationHandler<AddCommentCommand>
    {
        private readonly ILogger<AddCommentHandler> logger;
        private readonly IRepository repository;

        public AddCommentHandler(
            ILogger<AddCommentHandler> logger,
            IRepository repository)
        {
            this.logger = Guard.IsNotNull(logger, nameof(logger));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public async Task Handle(AddCommentCommand notification, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(notification, nameof(notification));

            Question question = await this.repository.GetQuestionByIdAsync
                (notification.QuestionId, cancellationToken).ConfigureAwait(false);

            if (question == null)
            {
                // we have a domain rule that this handler only handles adding comments and that the question must
                // already exist.
                throw new QuestionNotFoundException();
            }

            this.logger.LogInformation(notification.Description);

            notification.CommentId = question.AddComment(
                notification.CommentText,
                notification.Author, 
                notification.ParentId);

            await this.repository.UpdateQuestionAsync(question, cancellationToken).ConfigureAwait(false);
        }
    }
}