namespace PostMortem.Domain.Questions.Events
{
    using MediatR;

    public class QuestionTextUpdated : INotification
    {
        public Question Question { get; }

        public QuestionTextUpdated(Question question)
        {
            Question = question;
        }
    }
}