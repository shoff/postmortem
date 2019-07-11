namespace PostMortem.Domain.Events.Questions
{
    using Domain.Questions;

    public class QuestionCommentDeletedEventArgs : EventBase<Question>
    {
        public override Question Apply(Question t)
        {
            throw new System.NotImplementedException();
        }

        public override Question Undo(Question t)
        {
            throw new System.NotImplementedException();
        }
    }
}