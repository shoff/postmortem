using System.Linq;
using System.Threading.Tasks;
using Polly;
using PostMortem.Data.MongoDb;
using PostMortem.Domain.Events.Comments;
using PostMortem.Domain.EventSourcing.Events;

namespace PostMortem.Domain.Questions
{
    using System;
    using System.Collections.Generic;
    using ChaosMonkey.Guards;
    using Comments;
    using Events.Questions;


    public class Question : IEntity<QuestionId>
    {
        //private IEventBroker eventBroker;
        IQuestionRepository repository;

        public Question(IQuestionRepository repository)
        {
            
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
            var comment = new Comment(commentId, this.QuestionId, commenter, commentText, dateAdded);
            this.comments.Add(comment);
            // TODO wrap in polly policy and save the question and comment.
            throw new NotImplementedException();
        }

        public QuestionId GetEntityId() => QuestionId;
    }
}