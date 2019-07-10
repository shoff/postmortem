﻿namespace PostMortem.Domain.Events.Questions
{
    using Domain.Projects;
    using Domain.Questions;

    public class QuestionDeletedEventArgs : QuestionEventArgs {
        public QuestionDeletedEventArgs(Project project, Question question) 
            : base(project, question)
        {
        }
    }
}