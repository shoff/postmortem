namespace PostMortem.Domain.Events.Questions
{
    using Domain.Projects;
    using Domain.Questions;

    public class QuestionUpdatedEventArgs : QuestionEventArgs
    {
        public QuestionUpdatedEventArgs(Project project, Question question)
            : base(project, question)
        {
        }
    }
}