using System.Linq;
using Microsoft.Extensions.Logging;
using PostMortem.Domain.Projects.Commands;
using PostMortem.Domain.Questions.Commands;
using PostMortem.Domain.Questions.Queries;
using PostMortem.Infrastructure;

namespace PostMortem.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Domain;
    using Domain.Projects;
    using Dtos;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Polly;
    using Zatoichi.Common.Infrastructure.Extensions;

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : BaseController
    {
        private readonly IMediator mediator;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILogger<ProjectsController> logger;

        public ProjectsController(
            ILogger<ProjectsController> logger,
            IMediator mediator,
            IMapper mapper,
            INameGeneratorClient nameGenerator,
            LinkGenerator linkGenerator,
            IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor, null)
        {
            this.mediator = Guard.IsNotNull(mediator, nameof(mediator));
            this.linkGenerator = Guard.IsNotNull(linkGenerator, nameof(linkGenerator));
            this.mapper = Guard.IsNotNull(mapper, nameof(mapper));
            this.logger = Guard.IsNotNull(logger, nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await this.mediator.Send(new GetAllProjectsQueryArgs());
            if (result.Outcome == OutcomeType.Successful)
            {
                var projects = result.Result?.Select(p => this.mapper.Map<ProjectDto>(p))  ?? new ProjectDto[]{};
                return this.Ok(projects);
            }
            logger.LogError(500,$"{result.Outcome} : {result.FaultType}");
            logger.LogDebug(500,result.FinalException,$"{result.Outcome} : {result.FaultType}");
            return new StatusCodeResult(500);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await this.mediator.Send(new GetProjectByIdQueryArgs(id));
            if (result.Outcome == OutcomeType.Successful)
            {
                var project = this.mapper.Map<ProjectDto>(result.Result);
                return this.Ok(project);
            }
            logger.LogError(500,$"{result.Outcome} : {result.FaultType}");
            logger.LogDebug(500,result.FinalException,$"{result.Outcome} : {result.FaultType}");
            return new StatusCodeResult(500);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateProjectDto project)
        {
            Guard.IsNotNull(project, nameof(project));
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            project.CreatedBy = this.username;

            var cmd = new CreateProjectCommandArgs
            {
                ProjectId = new ProjectId(Guid.NewGuid()),
                EndDate = project.EndDate,
                ProjectName = project.ProjectName,
                StartDate = project.StartDate
            };
            var p = new ProjectDto
            {
                ProjectId = cmd.ProjectId,
                EndDate = cmd.EndDate,
                ProjectName = cmd.ProjectName,
                StartDate = cmd.StartDate
            };
            var result = await this.mediator.Send(cmd);

            if (result.Outcome == OutcomeType.Successful)
            {
                var url = this.linkGenerator.GetPathByAction(
                    this.HttpContext,
                    controller: "Projects",
                    action: "GetById",
                    values: new { id = cmd.ProjectId });
                return this.Created($"{this.HttpContext.Request.Scheme}//{this.HttpContext.Request.Host}{url}", p);
            }

            logger.LogError(500,$"{result.Outcome} : {result.ExceptionType}");
            logger.LogDebug(500,result.FinalException,$"{result.Outcome} : {result.ExceptionType}");
            return new StatusCodeResult(500);
        }

        [HttpGet("{projectId}/questions")]
        public async Task<IActionResult> GetQuestionsForProject(Guid projectId)
        {
            var result = await this.mediator.Send(new GetQuestionsForProjectIdQueryArgs{ProjectId = projectId});
            if (result.Outcome == OutcomeType.Successful)
            {
                var questions = result.Result?.Select(q => this.mapper.Map<QuestionDto>(q)) ?? new QuestionDto[]{};
                return this.Ok(questions);
            }

            logger.LogError(500,$"{result.Outcome} : {result.FaultType}");
            logger.LogDebug(500,result.FinalException,$"{result.Outcome} : {result.FaultType}");
            return new StatusCodeResult(500);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await this.mediator.Send(new DeleteProjectCommandArgs{ProjectId = id});
            if (result.Outcome == OutcomeType.Successful)
            {
                return this.Ok();
            }

            logger.LogError(500,$"{result.Outcome} : {result.ExceptionType}");
            logger.LogDebug(500,result.FinalException,$"{result.Outcome} : {result.ExceptionType}");
            return new StatusCodeResult(500);
        }

    }

}