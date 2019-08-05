namespace PostMortem.Infrastructure.Comments
{
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Domain;
    using Domain.Comments.Commands;
    using Polly;
    using Zatoichi.Common.Infrastructure.Resilience;
    using Zatoichi.EventSourcing.Commands;

    public class CommentLikedHandler : ICommand
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly IRepository repository;

        public CommentLikedHandler(
            IRepository repository,
            IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public Task<PolicyResult> Handle(CommentLikedEvent request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.LikeCommentAsync(request.CommentId));
        }

        public string Description { get; set; } = "Comment liked command.";
    }
}