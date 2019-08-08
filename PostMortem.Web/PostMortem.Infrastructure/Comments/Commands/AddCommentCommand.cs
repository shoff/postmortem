namespace PostMortem.Infrastructure.Comments.Commands
{
    using System;
    using Domain;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing.Commands;

    public class AddCommentCommand : ICommand
    {
        public AddCommentCommand(
            Guid questionId,
            string author,
            string commentText,
            Guid? parentId = null)
        {
            this.QuestionId = questionId;
            this.Author = string.IsNullOrWhiteSpace(author) ? Constants.ANONYMOUS_COWARD : author;
            this.CommentText = commentText;
            this.ParentId = parentId;
            this.Description = $"{Author} issued a command to add a comment to question {questionId}";
        }
        [JsonProperty]
        public Guid? CommentId { get; set; }

        [JsonProperty]
        public Guid? ParentId { get; private set; }

        [JsonProperty]
        public string CommentText { get; private set; }

        [JsonProperty]
        public Guid QuestionId { get; private set; }

        [JsonProperty]
        public string Author { get; private set; }

        [JsonProperty]
        public string Description { get; set; }
    }
}