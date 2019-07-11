
namespace PostMortem.Data.NEventStore
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;
    using Domain.Comments;
    using Domain.Projects;
    using Domain.Questions;
    using Polly;
    using global::NEventStore;
    using ChaosMonkey.Guards;
    using DomainProject=Domain.Projects.Project;
    using DomainQuestion=Domain.Questions.Question;
    using DomainComment=Domain.Comments.Comment;

    public partial class NEventStoreRepository : IRepository//, IDisposable
    {
        private readonly IStoreEvents eventStore;
        private readonly ILogger<NEventStoreRepository> logger;
        public NEventStoreRepository(IStoreEvents eventStore, ILogger<NEventStoreRepository> logger)
        {
            this.logger = Guard.IsNotNull(logger,nameof(logger));
            this.eventStore = Guard.IsNotNull(eventStore,nameof(eventStore));
        }
        public Task<PolicyResult<ICollection<DomainProject>>> GetAllProjectsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> CreateProjectAsync(DomainProject project)
        {
            //throw new NotImplementedException();

        }

        public Task<DomainProject> GetByProjectIdAsync(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> AddCommentAsync(DomainComment comment)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> AddQuestionAsync(DomainQuestion question)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> DeleteQuestionAsync(Guid questionId)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> DeleteCommentAsync(Guid questionId)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult<ICollection<DomainQuestion>>> GetQuestionsByProjectIdAsync(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> UpdateCommentAsync(DomainComment comment)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> LikeCommentAsync(Guid commentId)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> DislikeCommentAsync(Guid commentId)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> UpdateQuestionAsync(DomainQuestion question)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> UpdateProjectAsync(DomainProject requestProject)
        {
            throw new NotImplementedException();
        }
    }
}