﻿namespace PostMortem.Domain.Events.Questions
{
    using Domain.Questions;

    public class QuestionCommentAddedEventArgs : Command<Question>
    {
        public override Question Apply(Question t)
        {
            throw new System.NotImplementedException();
        }

        public override T Apply<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}