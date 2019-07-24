using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using PostMortem.Data.MongoDb.Config;
using PostMortem.Domain.Comments;
using PostMortem.Domain.Questions;
using DomainComment = PostMortem.Domain.Comments.Comment;

namespace PostMortem.Data.MongoDb
{
    public class CommentRepository : MongoDbRepositoryBase<DomainComment,CommentId,Comment, Guid>,ICommentRepository
    {
        public CommentRepository(IMongoDbContext context, IMapper mapper, ILogger<CommentRepository> logger) 
            : base(context?.Database, mapper, logger, Constants.COMMENTS_COLLECTION)
        {
        }

        public override CommentId GetEntityId(Comment dto) => dto.CommentId;
        public override Guid GetDtoId(Comment dto) => dto.CommentId;

        public async Task<IEnumerable<DomainComment>> GetCommentsForQuestionAsync(QuestionId questionId)
        {
            return await FindAllAsync(c => c.QuestionId == questionId);
        }
    }
}