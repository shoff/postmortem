using System.Threading.Tasks;
using Polly;
using PostMortem.Domain.Events.Comments;
using PostMortem.Domain.EventSourcing.Events;

namespace PostMortem.Domain.Questions
{
    using System;
    using System.Collections.Generic;
    using ChaosMonkey.Guards;
    using Comments;
    using Events.Questions;


    public class Question
    {
        private IEventBroker eventBroker;
        IRepository repository;

        public Question(IEventBroker eventBroker, IRepository repository)
        {
            this.eventBroker = Guard.IsNotNull(eventBroker, nameof(eventBroker));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        private readonly CommentCollection comments = new CommentCollection();

        public Question()
            : this(new List<Comment>()) { }

        public Question(Guid questionId)
        {
            QuestionId = new QuestionId(questionId);
        }

        public Question(ICollection<Comment> comments)
        {
            Guard.IsNotNull(comments, nameof(comments));
            this.comments.AddRange(comments);
        }

        public QuestionId QuestionId { get; set; }
        public Guid ProjectId { get; set; }
        public string QuestionText { get; set; }
        public int ResponseCount { get; set; }
        public int Importance { get; set; }
        public bool Active { get; set; } = true;
        public DateTime? LastUpdated { get; set; }

        public IReadOnlyCollection<Comment> Comments
        {
            get
            {
                if (this.QuestionId.Equals(QuestionId.Empty))
                {
                    this.QuestionId = QuestionId.NewQuestionId();
                }

                this.comments.QuestionId = this.QuestionId.Id;
                return this.comments;
            }
        }

        public async Task<PolicyResult<Comment>> AddCommentAsync(Guid commentId,string commentText, DateTime dateAdded, string commenter )
        {
            // To avoid full CQRS, call the repository here? (Repo will call the event store to reconstitute the comment)... feels like a hack tho.
            // fire off the comment added event here 
            var comment = new Comment
            {
                CommentId = new CommentId(commentId), Commenter = commenter, CommentText = commentText,
                DateAdded = dateAdded, Likes = 0, Dislikes = 0, QuestionId = this.QuestionId.Id,
                GenerallyPositive = false
            };
            var _ = await repository.AddCommentAsync(comment);
            
            eventBroker.RaiseEvent(new CommentAddedEventArgs(commentId) { CommentText = commentText, Commenter = commenter, QuestionId = this.QuestionId.Id});
            // TODO wrap in polly policy
            throw new NotImplementedException();
        }


        //public static QuestionAddedEventArgs CreateQuestionAddedEventArgs(Question question)
        //{
        //    QuestionAddedEventArgs eventArgs = new QuestionAddedEventArgs(question);
        //    return eventArgs;
        //}

        //public static QuestionDeletedEventArgs CreateQuestionDeletedEventArgs(Question question)
        //{
        //    var eventArgs = new QuestionDeletedEventArgs(question.QuestionId);
        //    return eventArgs;
        //}

        //public static QuestionUpdatedEventArgs CreateQuestionUpdatedEventArgs(Question question)
        //{
        //    var eventArgs = new QuestionUpdatedEventArgs(question);
        //    return eventArgs;
        //}
    }
}