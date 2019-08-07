namespace PostMortem.Infrastructure.Comments
{
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Commands;
    using Domain;
    using MediatR;
    using Polly;
    using Zatoichi.Common.Infrastructure.Resilience;

    public class CommentUpdatedHandler : INotificationHandler<AddCommentCommand>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly IRepository repository;

        public CommentUpdatedHandler(
            IRepository repository,
            IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public Task Handle(AddCommentCommand notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}