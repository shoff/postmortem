namespace PostMortem.Infrastructure.Questions
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Dtos;
    using MediatR;
    using Queries;

    public class GetQuestionByIdHandler : IRequestHandler<GetQuestionByIdQuery, QuestionDto>
    {
        private readonly IMapper mapper;
        private readonly IRepository repository;

        public GetQuestionByIdHandler(
            IMapper mapper,
            IRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<QuestionDto> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await this.repository.GetQuestionByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<QuestionDto>(entity);
        }
    }
}