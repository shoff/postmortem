namespace PostMortem.Domain.Comments
{
    using System;
    using Events.Comments;

    public class Comment
    {
        public Guid CommentId { get; set; }
        public Guid QuestionId { get; set; }
        public string CommentText { get; set; }
        public bool GenerallyPositive { get; set; }
        public DateTime DateAdded { get; set; }
        public string Commenter { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public static CommentGetByIdEventArgs CreateGetByIdEventArgs(Guid commentId)
        {
            var eventArgs = new CommentGetByIdEventArgs(commentId);
            return eventArgs;
        }
        public static CommentAddedEventArgs CreateCommentAddedEventArgs(Comment comment)
        {
            var eventArgs = new CommentAddedEventArgs(comment);
            return eventArgs;
        }
        public static CommentLikedEventArgs CreateCommentLikedEventArgs(Comment comment)
        {
            var eventArgs = new CommentLikedEventArgs(comment.CommentId);
            return eventArgs;
        }
        public static CommentDislikedEventArgs CreateCommentDislikedEventArgs(Comment comment)
        {
            var eventArgs = new CommentDislikedEventArgs(comment.CommentId);
            return eventArgs;
        }
        public static CommentUpdatedEventArgs CreateCommentUpdatedEventArgs(Comment comment)
        {
            var eventArgs = new CommentUpdatedEventArgs(comment);
            return eventArgs;
        }
    }
}