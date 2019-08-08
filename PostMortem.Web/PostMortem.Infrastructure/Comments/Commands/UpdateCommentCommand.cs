namespace PostMortem.Infrastructure.Comments.Commands
{
    using System;
    using ChaosMonkey.Guards;
    using Domain;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing.Commands;

    public class UpdateCommentCommand : ICommand
    {
        public UpdateCommentCommand(
            Guid commentId, 
            Guid questionId, 
            string commentText,
            string author = null)
        {
            this.CommentText = commentText;
            this.CommentId = Guard.IsNotDefault(commentId, nameof(commentId));
            this.QuestionId = Guard.IsNotDefault(questionId, nameof(questionId));
            this.VoterId = string.IsNullOrWhiteSpace(author) ? Constants.ANONYMOUS_COWARD : author;
            this.Description = $"{this.VoterId} issued update command on comment {this.CommentId}";
        }
        [JsonProperty]
        public string CommentText { get; private set; }
        [JsonProperty]
        public Guid QuestionId { get; private set; }
        [JsonProperty]
        public Guid CommentId { get; private set; }
        [JsonProperty]
        public string VoterId { get; private set; }
        [JsonProperty]
        public string Description { get; set; }
    }
}