namespace PostMortem.Infrastructure.Comments
{
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Commands;
    using Domain;
    using Domain.Questions;
    using MediatR;
    using Zatoichi.Common.Infrastructure.Resilience;
    using Zatoichi.EventSourcing;

    public class CommentAddedHandler : INotificationHandler<AddCommentCommand>
    {
        private readonly IRepository repository;
        private readonly IExecutionPolicies executionPolicies;
        private readonly IEventStore eventStore;

        public CommentAddedHandler(
            IRepository repository,
            IEventStore eventStore,
            IExecutionPolicies executionPolicies)
        {
            this.repository = Guard.IsNotNull(repository, nameof(repository));
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.eventStore = Guard.IsNotNull(eventStore, nameof(eventStore));
        }

        public async Task Handle(AddCommentCommand notification, CancellationToken cancellationToken)
        {
            Question question = await this.repository.GetQuestionByIdAsync(notification.QuestionId);

            if (question == null)
            {
                // we have a domain rule that this handler only handles adding comments and that the question must
                // already exist.
                throw new QuestionNotFoundException();
            }
            question.AddComment(notification.CommentText, notification.Commenter, notification.ParentId);

        }
    }
}