namespace PostMortem.Infrastructure.Questions
{
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Commands;
    using Domain.Questions;
    using MediatR;
    using Microsoft.Extensions.Options;

    public class AddQuestionHandler : INotificationHandler<AddQuestionCommand>
    {
        private readonly IOptions<QuestionOptions> options;
        private readonly IRepository repository;

        public AddQuestionHandler(
            IRepository repository,
            IOptions<QuestionOptions> options)
        {
            this.options = Guard.IsNotNull(options, nameof(options));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public async Task Handle(AddQuestionCommand notification, CancellationToken cancellationToken)
        {
            var question = new Question(notification.QuestionText, this.options.Value, notification.ProjectId);
            await this.repository.AddQuestionAsync(question).ConfigureAwait(false);
        }
    }
}