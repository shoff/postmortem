using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using PostMortem.Data.MongoDb.Config;
using PostMortem.Domain;
using PostMortem.Domain.Projects;
using PostMortem.Domain.Questions;
using DomainQuestion=PostMortem.Domain.Questions.Question;

namespace PostMortem.Data.MongoDb
{
    public class QuestionRepository : MongoDbRepositoryBase<DomainQuestion, QuestionId, Question, Guid>, IQuestionRepository
    {
        public QuestionRepository(IMongoDbContext context, ILogger<QuestionRepository> logger, IMapper mapper)
            : base(context?.Database, mapper, logger, Constants.QUESTIONS_COLLECTION)
        {
        }

        public override Guid GetDtoId(Question dto) => dto.QuestionId;

        public async Task<IEnumerable<DomainQuestion>> GetQuestionsForProjectAsync(ProjectId projectId)
        {
            return await FindAllAsync(q => q.ProjectId == projectId);
        }
    }
}