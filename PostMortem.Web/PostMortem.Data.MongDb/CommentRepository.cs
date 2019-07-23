using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using PostMortem.Data.MongoDb.Config;
using PostMortem.Domain.Comments;
using PostMortem.Domain.Projects;
using DomainComment=PostMortem.Domain.Comments.Comment;
using DomainProject=PostMortem.Domain.Projects.Project;
using DomainQuestion=PostMortem.Domain.Questions.Question;

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
    }
}