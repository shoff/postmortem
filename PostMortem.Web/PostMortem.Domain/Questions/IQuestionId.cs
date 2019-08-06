namespace PostMortem.Domain.Questions
{
    using System;

    public interface IQuestionId
    {
        Guid Id { get; }
    }
}