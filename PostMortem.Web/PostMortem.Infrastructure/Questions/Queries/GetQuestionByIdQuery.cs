namespace PostMortem.Infrastructure.Questions.Queries
{
    using System;
    using Domain.Questions;
    using MediatR;

    public class GetQuestionByIdQuery : IRequest<Question>
    {

        public GetQuestionByIdQuery(Guid id)
        {
            this.Id = id;
        }
        public Guid Id { get; }
    }
}