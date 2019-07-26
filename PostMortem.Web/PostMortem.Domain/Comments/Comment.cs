using System.Collections.Generic;
using System.Threading.Tasks;
using ChaosMonkey.Guards;
using Polly;
using PostMortem.Domain.Comments.Events;
using PostMortem.Domain.Events.Comments;
using PostMortem.Infrastructure;
using PostMortem.Infrastructure.Events;


namespace PostMortem.Domain.Comments
{
    using System;

    public sealed class Comment : EventsEntityBase<CommentId,CommentEventArgsBase>
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

        public Comment(CommentId commentId, IEnumerable<CommentEventArgsBase> events) : base(events)
        {
            this.CommentId = commentId;
        }

        /// <summary>
        /// For initial creation from params
        /// </summary>
        public Comment(CommentId commentId, Guid questionId, string commenter, string commentText, DateTime? dateAdded=null, int likes=0, int dislikes=0, bool generallyPositive=true)
        {
            var initArgs = new CommentCreatedEventArgs(commentId, questionId, commenter, commentText, 
                dateAdded.HasValue ? dateAdded.Value : DateTime.Now,likes, dislikes, generallyPositive);
            Initialize(initArgs);
            AppendEvent(initArgs);
        }

        private CommentId commentId = CommentId.Empty;
        private string commentText;
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
                if (oldValue != value)
                {
                    AppendEvent(new CommentTextSetArgs(this.CommentId, oldValue, value));
                }
            }
        }

        public DateTime DateAdded { get; private set; }

        public string Commenter { get; private set; }

        public int Likes { get; private set; }

        public int Dislikes { get; private set; }

        public bool GenerallyPositive
        {
            get => generallyPositive;
            set
            {
                var oldValue = generallyPositive;
                generallyPositive = value;
                if (oldValue != value)
                {
                    AppendEvent(new CommentGenerallyPositiveSetArgs(this.CommentId, oldValue, value));
                }
            }
        }

        public override CommentId GetEntityId() => CommentId;
        public override void ReplayEvent(CommentEventArgsBase eventArgs)
        {
            // replaying an event should NOT regenerate the event.
            switch (eventArgs)
            {
                case CommentCreatedEventArgs ca:
                    Initialize(ca);
                    break;
                case CommentGenerallyPositiveSetArgs gps:
                    generallyPositive = gps.NewValue;
                    break;
                case CommentLikedEventArgs cl:
                    Likes++;
                    break;
                case CommentDislikedEventArgs cdl:
                    Dislikes++;
                    break;
                case CommentTextSetArgs cts:
                    commentText = cts.NewValue;
                    break;
            }
        }

        private void Initialize(CommentCreatedEventArgs initArgs)
        {
            this.CommentId = initArgs.CommentId;
            this.QuestionId = initArgs.QuestionId;
            this.Commenter = initArgs.Commenter;
            this.DateAdded = initArgs.DateAdded;
            this.commentText = initArgs.CommentText;
            this.Likes = initArgs.Likes;
            this.Dislikes = initArgs.Dislikes;
            this.generallyPositive = initArgs.GenerallyPositive;
        }

        public void Like()
        {
            Likes++;
            SetGenerallyPositive();
            AppendEvent(new CommentLikedEventArgs(this.CommentId));
        }

        private void SetGenerallyPositive()
        {
            GenerallyPositive = Likes >= Dislikes;
        }

        public void Dislike()
        {
            Dislikes++;
            SetGenerallyPositive();
            AppendEvent(new CommentDislikedEventArgs(this.CommentId));
        }


    }
}