namespace PostMortem.Domain.Questions
{
    using System;
    using System.Collections.Generic;
    using ChaosMonkey.Guards;
    using Comments;
    using Events;
    using Projects;


    public class Question
    {
        private readonly int maximumQuestionLength;
        private readonly CommentCollection comments = new CommentCollection();

        public Question(Project project)
        {
            Guard.IsNotNull(project, nameof(project));
            this.Options = project.GetOptions().Value;
            this.maximumQuestionLength = this.Options.QuestionMaximumLength;
            this.ProjectId = project.ProjectId;
        }

        public QuestionUpdatedEvent AddQuestionText(string text)
        {
            Guard.IsLessThan(text?.Length ?? 0, this.maximumQuestionLength, nameof(text));
            this.QuestionText = text;
            return CreateQuestionUpdatedEvent(this);
        }

        public Guid QuestionId { get; set; }

        public Guid ProjectId { get; }

        public string QuestionText { get; private set; } = string.Empty;

        public int ResponseCount => this.comments.Count;

        public int Importance { get; set; } 

        
        internal QuestionOptions Options { get; }

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



        public static QuestionAddedEvent CreateQuestionAddedEventArgs(Question question)
        {
            QuestionAddedEvent questionAddedEvent = new QuestionAddedEvent(question);
            return questionAddedEvent;
        }

        public static QuestionDeletedEventArgs CreateQuestionDeletedEventArgs(Question question)
        {
            var eventArgs = new QuestionDeletedEventArgs(question);
            return eventArgs;
        }

        private static QuestionUpdatedEvent CreateQuestionUpdatedEvent(Question question)
        {
            var eventArgs = new QuestionUpdatedEvent(question);
            return eventArgs;
        }
    }
}