namespace PostMortem.Domain.Events.Questions
{
    using Domain.Questions;

    public class QuestionUpdatedEventArgs : QuestionEventArgs
    {
        public QuestionUpdatedEventArgs(Question question)
            : base(question)
        {
        }
    }
}