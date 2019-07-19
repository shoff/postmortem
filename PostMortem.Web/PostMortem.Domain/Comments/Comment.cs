using System.Threading.Tasks;
using ChaosMonkey.Guards;
using Polly;
using PostMortem.Domain.EventSourcing.Events;

namespace PostMortem.Domain.Comments
{
    using System;
    using Events.Comments;

    public class Comment
    {
        private IEventBroker eventBroker;
        IRepository repository;
        public Comment(){}
        public Comment(IEventBroker eventBroker, IRepository repository)
        {
            this.eventBroker = Guard.IsNotNull(eventBroker,nameof(eventBroker));
            this.repository = Guard.IsNotNull(repository,nameof(repository));
        }

        private CommentId commentId = CommentId.Empty;

        public CommentId CommentId
        {
            get
            {
                if (this.commentId.Equals(CommentId.Empty))
                {
                    this.commentId = CommentId.NewCommentId();
                }

                return this.commentId;
            }
            set => this.commentId = value;
        }

        public Guid QuestionId { get; set; }
        public string CommentText { get; set; }
        public DateTime DateAdded { get; set; }
        public string Commenter { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public bool GenerallyPositive { get; set; }

        /// <summary>
        /// To avoid full CQRS, call the repository here? (Repo will call the event store to reconstitute the comment)... feels like a hack tho.
        /// </summary>
        public Task<PolicyResult<Comment>> GetCommentById(Guid commentId)
        {
            throw new NotImplementedException();
        }


        //public static CommentGetByIdEventArgs CreateGetByIdEventArgs(Guid commentId)
        //{
        //    var eventArgs = new CommentGetByIdEventArgs(commentId);
        //    return eventArgs;
        //}
        //public static CommentAddedEventArgs CreateCommentAddedEventArgs(Comment comment)
        //{
        //    var eventArgs = new CommentAddedEventArgs(comment);
        //    return eventArgs;
        //}
        //public static CommentLikedEventArgs CreateCommentLikedEventArgs(Comment comment)
        //{
        //    var eventArgs = new CommentLikedEventArgs(comment.CommentId.Id);
        //    return eventArgs;
        //}
        //public static CommentDislikedEventArgs CreateCommentDislikedEventArgs(Comment comment)
        //{
        //    var eventArgs = new CommentDislikedEventArgs(comment.CommentId.Id);
        //    return eventArgs;
        //}
        //public static CommentUpdatedEventArgs CreateCommentUpdatedEventArgs(Comment comment)
        //{
        //    var eventArgs = new CommentUpdatedEventArgs(comment);
        //    return eventArgs;
        //}
    }
}