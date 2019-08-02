namespace PostMortem.Domain
{
    using ChaosMonkey.Guards;
    using Projects;
    using Questions;

    public class QuestionBuilder
    {
        private readonly IRepository repository;

        public QuestionBuilder(IRepository repository)
        {
            this.repository = repository;
        }

        public Question Build(Project project)
        {
            Guard.IsNotNull(project, nameof(project));
            return new Question(project.GetOptions());
        }
    }
}