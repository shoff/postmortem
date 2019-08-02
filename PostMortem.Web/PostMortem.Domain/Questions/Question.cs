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

        public Question AddQuestionText(string text)
        {

        }

        public Guid QuestionId { get; set; }

        public Guid ProjectId { get; }

        public string QuestionText { get; }

        public int ResponseCount { get;  }

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



        public static QuestionAddedEventArgs CreateQuestionAddedEventArgs(Question question)
        {
            QuestionAddedEventArgs eventArgs = new QuestionAddedEventArgs(question);
            return eventArgs;
        }

        public static QuestionDeletedEventArgs CreateQuestionDeletedEventArgs(Question question)
        {
            var eventArgs = new QuestionDeletedEventArgs(question);
            return eventArgs;
        }

        public static QuestionUpdatedEventArgs CreateQuestionUpdatedEventArgs(Question question)
        {
            var eventArgs = new QuestionUpdatedEventArgs(question);
            return eventArgs;
        }
    }
}