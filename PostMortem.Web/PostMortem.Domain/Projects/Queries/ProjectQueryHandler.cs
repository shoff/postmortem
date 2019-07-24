using System.Collections.Generic;
using PostMortem.Domain.Comments;
using PostMortem.Domain.EventSourcing.Queries;
using PostMortem.Domain.Projects;
using PostMortem.Domain.Questions;

namespace PostMortem.Infrastructure.Events.Projects
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Domain;
    using MediatR;
    using Polly;
    using Zatoichi.Common.Infrastructure.Resilience;

    public class ProjectQueryHandler : 
        IQueryHandler<GetAllProjectsQueryArgs,IEnumerable<Project>>,
        IQueryHandler<GetProjectByIdQueryArgs,Project>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly IProjectRepository projectRepository;
        private IQuestionRepository questionRepository;
        private ICommentRepository commentRepository;

        public ProjectQueryHandler(
            IProjectRepository projectRepository,
            IQuestionRepository questionRepository,
            ICommentRepository commentRepository,
            IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.projectRepository = Guard.IsNotNull(projectRepository, nameof(projectRepository));
            this.questionRepository = Guard.IsNotNull(questionRepository, nameof(questionRepository));
            this.commentRepository = Guard.IsNotNull(commentRepository, nameof(commentRepository));
        }

        public async Task<PolicyResult<IEnumerable<Project>>> Handle(GetAllProjectsQueryArgs request, CancellationToken cancellationToken)
        {
            return await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.projectRepository.GetAllAsync());
        }

        public async Task<PolicyResult<Project>> Handle(GetProjectByIdQueryArgs request, CancellationToken cancellationToken)
        {
            // TODO: do error checking.
            var project = await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.projectRepository.GetByIdAsync(request.ProjectId));
            var questionsPolicyResult = await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.questionRepository.GetQuestionsForProjectAsync(request.ProjectId));
            var questions = questionsPolicyResult.Result;
            // project.AttachQuestions(questions)
            foreach (var question in questions)
            {
                var commentsPolicyResult = await this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() =>
                    this.commentRepository.GetCommentsForQuestionAsync(question.QuestionId));
                var comments = commentsPolicyResult.Result;
                //question.AttachComments(comments)
            }
            return project;
        }
    }
}