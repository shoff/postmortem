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
        private readonly IRepository repository;

        public AddQuestionHandler(
            IRepository repository)
        {
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public async Task Handle(AddQuestionCommand notification, CancellationToken cancellationToken)
        {
            /*
                string questionText,
                Guid projectId,
                string author,
                Guid? questionId = null
             */
            var question = new Question(
                notification.QuestionText, 
                notification.ProjectId,
                notification.Author);
            notification.QuestionId = question.QuestionId;
            await this.repository.AddQuestionAsync(question).ConfigureAwait(false);
        }
    }
}