namespace PostMortem.Infrastructure.Comments
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Commands;
    using Domain.Questions;
    using MediatR;

    public class AddCommentHandler : INotificationHandler<AddCommentCommand>
    {
        private readonly IRepository repository;

        public AddCommentHandler(
            IRepository repository)
        {
            this.repository = Guard.IsNotNull(repository, nameof(repository));
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