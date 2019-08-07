namespace PostMortem.Infrastructure.Comments.Queries
{
    using System;

    public class GetCommentByIdQuery : CommentQueryEvent
    {
        
        public GetCommentByIdQuery(Guid questionId, Guid commentId)
        {
            // in this case do we NEED a question aggregate root just to query for a comment?
            this.QuestionId = questionId;
            this.CommentId = commentId;
            this.Description = "Gets a comment by id";
        }

        public Guid QuestionId { get; }
        public Guid CommentId { get; }
    }
}