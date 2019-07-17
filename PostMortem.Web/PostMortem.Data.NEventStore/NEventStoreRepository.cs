
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
        public async Task<ICollection<DomainProject>> GetAllProjectsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> CreateProjectAsync(DomainProject project)
        {
            throw new NotImplementedException();
        }

        public async Task<DomainProject> GetByProjectIdAsync(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> AddCommentAsync(DomainComment comment)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> AddQuestionAsync(DomainQuestion question)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteQuestionAsync(Guid questionId)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteCommentAsync(Guid questionId)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<DomainQuestion>> GetQuestionsByProjectIdAsync(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCommentAsync(DomainComment comment)
        {
            throw new NotImplementedException();
        }

        public async Task LikeCommentAsync(Guid commentId)
        {
            throw new NotImplementedException();
        }

        public async Task DislikeCommentAsync(Guid commentId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateQuestionAsync(DomainQuestion question)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateProjectAsync(DomainProject requestProject)
        {
            throw new NotImplementedException();
        }

        public Task UpdateQuestionResponseCount(Guid projectId, int count)
        {
            throw new NotImplementedException();
        }
    }
}