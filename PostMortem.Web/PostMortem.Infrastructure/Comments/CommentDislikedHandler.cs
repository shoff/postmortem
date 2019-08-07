namespace PostMortem.Infrastructure.Comments
{
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Commands;
    using Domain;
    using MediatR;
    using Zatoichi.Common.Infrastructure.Resilience;

    public class CommentDislikedHandler : INotificationHandler<DislikeCommentCommand>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly IRepository repository;

        public CommentDislikedHandler (
            IRepository repository,
            IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public Task Handle(DislikeCommentCommand notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}