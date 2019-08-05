namespace PostMortem.Domain.Questions
{
    public class QuestionOptions
    {
        public int MaximumNumberOfComments { get; set; }
        public int QuestionMaximumLength { get; set; }
        public int CommentMaximumLength { get; set; }
        public int MaximumLikesPerCommentPerVoter { get; set; }
        public int MaximumDisLikesPerCommentPerVoter { get; set; }
    }
}