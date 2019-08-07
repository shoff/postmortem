namespace PostMortem.Infrastructure.Comments.Commands
{
    using System;
    using ChaosMonkey.Guards;
    using Domain.Voters;
    using Dtos;
    using Newtonsoft.Json;

    public class AddCommentCommand : CommentCommand
    {
        public AddCommentCommand(
            CommentDto comment,
            IVoterId voterId)
            : base(comment.CommentId)
        {
            this.QuestionId = comment.QuestionId;
            this.VoterId = Guard.IsNotNull(voterId, nameof(voterId));
            this.CommentText = comment.CommentText;
            this.Commenter = comment.Commenter;
            this.ParentId = comment.ParentId;
        }

        [JsonProperty]
        public Guid? ParentId { get; private set; }

        [JsonProperty]
        public string Commenter { get; private set; }

        [JsonProperty]
        public string CommentText { get; private set; }

        [JsonProperty]
        public Guid QuestionId { get; private set; }

        [JsonProperty]
        public IVoterId VoterId { get; private set; }
    }
}