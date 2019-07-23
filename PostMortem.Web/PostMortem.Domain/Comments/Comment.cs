using System.Threading.Tasks;
using ChaosMonkey.Guards;
using Polly;
using PostMortem.Data.MongoDb;
using PostMortem.Domain.EventSourcing.Events;

namespace PostMortem.Domain.Comments
{
    using System;
    using Events.Comments;

    public class Comment : EventsEntityBase<CommentId,CommentEventArgsBase>
    {
        // for serialization
        public Comment(){}

        /// <summary>
        ///  for replaying.
        /// </summary>
        /// <param name="commentId"></param>
        public Comment(CommentId commentId) 
        {
            this.CommentId = commentId;
        }
        /// <summary>
        /// For initial creation from params
        /// </summary>
        public Comment(CommentId commentId, Guid questionId, string commenter, string commentText, DateTime? dateAdded=null)
        {
            var initiArgs = new CommentCreatedEventArgs(commentId, questionId, commenter, commentText, dateAdded.HasValue ? dateAdded.Value : DateTime.Now);
            Initialize(initiArgs);
            AppendEvent(initiArgs);
        }

        private CommentId commentId = CommentId.Empty;
        private string commentText;
        private DateTime dateAdded;
        private string commenter;
        private int likes;
        private int dislikes;
        private bool generallyPositive;

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
            private set => this.commentId = value;
        }

        public Guid QuestionId { get;  private set; }

        public string CommentText
        {
            get => commentText;
            set
            {
                var oldValue = commentText;
                commentText = value;
                AppendEvent(new CommentTextSetArgs(this.CommentId,oldValue,value));
            }
        }

        public DateTime DateAdded
        {
            get => dateAdded;
            set => dateAdded = value;
        }

        public string Commenter
        {
            get => commenter;
            set => commenter = value;
        }

        public int Likes
        {
            get => likes;
        }

        public int Dislikes
        {
            get => dislikes;
        }
        public bool GenerallyPositive
        {
            get => generallyPositive;
            set => generallyPositive = value;
        }
        public override CommentId GetEntityId() => CommentId;
        public override void ReplayEvent(CommentEventArgsBase eventArgs)
        {
            switch (eventArgs)
            {
                case CommentCreatedEventArgs ca:
                    Initialize(ca);
                    break;
                case CommentGenerallyPositiveSetArgs gps:
                    generallyPositive = gps.NewValue;
                    break;
                case CommentLikedEventArgs cl:
                    likes++;
                    break;
                case CommentDislikedEventArgs cdl:
                    dislikes++;
                    break;
                case CommentTextSetArgs cts:
                    commentText = cts.NewValue;
                    break;
            }
        }

        private void Initialize(CommentCreatedEventArgs initArgs)
        {
            this.QuestionId = initArgs.QuestionId;
            this.commenter = initArgs.Commenter;
            this.dateAdded = initArgs.DateAdded;
            this.commentText = initArgs.CommentText;
        }

        public void Like()
        {
            likes++;
            AppendEvent(new CommentLikedEventArgs(this.CommentId));
        }

        public void Dislike()
        {
            dislikes++;
            AppendEvent(new CommentDislikedEventArgs(this.CommentId));
        }


    }
}