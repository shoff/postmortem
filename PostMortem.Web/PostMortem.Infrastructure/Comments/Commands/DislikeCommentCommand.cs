namespace PostMortem.Infrastructure.Comments.Commands
{
    using System;
    using ChaosMonkey.Guards;
    using Domain;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing.Commands;

    public class DislikeCommentCommand : ICommand
    {

        public DislikeCommentCommand(Guid commentId, Guid questionId, string author = null)
        {
            this.CommentId = Guard.IsNotDefault(commentId, nameof(commentId));
            this.QuestionId = Guard.IsNotDefault(questionId, nameof(questionId));
            this.VoterId = string.IsNullOrWhiteSpace(author) ? Constants.ANONYMOUS_COWARD : author;
            this.Description = $"{this.VoterId} disliked the comment {this.CommentId}";
        }

        [JsonProperty]
        public Guid QuestionId { get; set; }

        [JsonProperty]
        public Guid CommentId { get; private set; }

        [JsonProperty]
        public string VoterId { get; private set; }

        [JsonProperty]
        public string Description { get; set; }
    }
}