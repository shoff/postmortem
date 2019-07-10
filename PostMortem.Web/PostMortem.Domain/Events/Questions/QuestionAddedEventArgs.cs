namespace PostMortem.Domain.Events.Questions
{
    using Domain.Projects;
    using Domain.Questions;
    using Projects;

    public class QuestionAddedEventArgs : QuestionEventArgs
    {
        public QuestionAddedEventArgs(Project project, Question question)
            : base(project, question)
        {
        }
    }
}