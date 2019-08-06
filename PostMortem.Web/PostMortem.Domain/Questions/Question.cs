namespace PostMortem.Domain.Questions
{
    using System;
    using System.Collections.Generic;
    using ChaosMonkey.Guards;
    using Commands;
    using Comments;
    using Comments.Commands;
    using Newtonsoft.Json;
    using Projects;
    using Zatoichi.EventSourcing;

    public sealed class Question : Aggregate
    {
        private readonly int maximumQuestionLength;
        private readonly CommentCollection comments = new CommentCollection();

        [JsonConstructor]
        public Question() { }

        public Question(Project project, Guid? id)
        {
            Guard.IsNotNull(project, nameof(project));
            this.Options = project.GetOptions().Value;
            this.maximumQuestionLength = this.Options.QuestionMaximumLength;
            this.ProjectId = project.ProjectId;
            this.QuestionId = id ?? Guid.NewGuid();
        }

        public void AddQuestionText(string text)
        {
            // Fowler states that the entity shouldn't validate things like string length,
            // but well Fowler is a Brit and drinks tea, so we don't care what he says. :D
            // Actually, we really shouldn't because if the spec changes then we have to 
            // update our entity and then it becomes impossible to recreate from older events 
            // without doing some crazy stuff
            Guard.IsLessThan(text?.Length ?? 0, this.maximumQuestionLength, nameof(text));
            this.QuestionText = text;
        }

        public AddCommentCommand AddComment(Comment comment)
        {
            this.comments.Add(comment);
            // Validation happens here
            return new AddCommentCommand(comment);
        }

        [JsonProperty]
        public Guid QuestionId { get; private set; }
        [JsonProperty]
        public Guid ProjectId { get; private set; }
        [JsonProperty]
        public string QuestionText { get; private set; } = string.Empty;

        public int ResponseCount => this.comments.Count;

        public int Importance { get; set; } 

        public QuestionOptions Options { get; internal set; }

        [JsonProperty]
        public IReadOnlyCollection<Comment> Comments
        {
            get
            {
                if (this.QuestionId == Guid.Empty)
                {
                    this.QuestionId = Guid.NewGuid();
                }

                this.comments.QuestionId = this.QuestionId;
                return this.comments;
            }
        }

        public static AddQuestionCommand CreateQuestionAddedEvent(Question question)
        {
            AddQuestionCommand addQuestionCommand = new AddQuestionCommand(question);
            return addQuestionCommand;
        }

        public static DeleteQuestionCommand CreateQuestionDeletedEvent(Question question)
        {
            var eventArgs = new DeleteQuestionCommand(question);
            return eventArgs;
        }

        private static UpdateQuestionCommand CreateQuestionUpdatedEvent(Question question)
        {
            var eventArgs = new UpdateQuestionCommand(question);
            return eventArgs;
        }

        public override void ClearPendingEvents()
        {
            // TODO this can be used as sort of "commit transaction" 
        }

        public override void ApplyEvents()
        {
            // only here to make debugging easier
            base.ApplyEvents();
        }
    }
}