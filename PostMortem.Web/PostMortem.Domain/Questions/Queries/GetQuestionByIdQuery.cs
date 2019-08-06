namespace PostMortem.Domain.Questions.Queries
{
    using System;
    using MediatR;

    public class GetQuestionByIdQuery : IRequest<Question>
    {

        public GetQuestionByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}