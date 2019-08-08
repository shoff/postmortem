namespace PostMortem.Infrastructure.Comments
{
    using System;
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

            Question question = await this.repository.GetQuestionByIdAsync(notification.QuestionId, cancellationToken);

            if (question == null)
            {
                // we have a domain rule that this handler only handles adding comments and that the question must
                // already exist.
                throw new QuestionNotFoundException();
            }

            this.logger.LogInformation(notification.Description);
            question.AddComment(notification.CommentText, notification.Author, notification.ParentId);

        }
    }
}