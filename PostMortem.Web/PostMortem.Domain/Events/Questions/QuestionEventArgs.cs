namespace PostMortem.Domain.Events.Questions
{
    using System;
    using ChaosMonkey.Guards;
    using Domain.Projects;
    using Domain.Questions;
    using MediatR;
    using Polly;

    public abstract class QuestionEventArgs : EventArgs, IRequest<PolicyResult>
    {
        protected QuestionEventArgs(Project project, Question question)
        {
            this.Project = Guard.IsNotNull(project, nameof(project));
            this.Question = Guard.IsNotNull(question, nameof(question));
        }

        public virtual DateTime CreatedDate => DateTime.UtcNow;
        public virtual Project Project { get; private set; }
        public virtual Question Question { get; private set; }
    }
}