namespace PostMortem.Infrastructure.Questions.Queries
{
    using System;
    using Dtos;
    using MediatR;

    public class GetQuestionByIdQuery : IRequest<QuestionDto>
    {
        public GetQuestionByIdQuery(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}