namespace PostMortem.Infrastructure.Questions
{
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Commands;
    using Domain.Questions.Events;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class UpdateQuestionHandler : INotificationHandler<UpdateQuestionCommand>
    {
        private readonly IRepository repository;
        private readonly ILogger<UpdateQuestionHandler> logger;

        public UpdateQuestionHandler(
            ILogger<UpdateQuestionHandler> logger,
            IRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        // ONLY updates the question text
        public async Task Handle(UpdateQuestionCommand notification, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(notification, nameof(notification));
            this.logger.LogInformation($"Handling command {notification.Description}");
            var question = await this.repository.GetQuestionByIdAsync(notification.QuestionId);

            // we ca
            question.QuestionTextUpdatedEvent += this.QuestionUpdateSucceeded;
            question.Update(notification.QuestionText, notification.Author);
            question.QuestionTextUpdatedEvent -= this.QuestionUpdateSucceeded;
        }

        private void QuestionUpdateSucceeded(object sender, QuestionUpdated e)
        {
            this.logger.LogInformation($"Domain event bubbled up {e.QuestionId}");
        }
    }
}