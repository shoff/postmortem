namespace PostMortem.Domain.Events.Questions
{
    using Domain.Questions;

    public class QuestionAddedEventArgs : QuestionEventArgs
    {
        public QuestionAddedEventArgs(Question question)
            : base(question)
        {
        }
    }
}