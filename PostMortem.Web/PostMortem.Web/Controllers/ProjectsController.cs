namespace PostMortem.Web.Controllers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Domain.Voters;
    using Dtos;
    using Infrastructure;
    using Infrastructure.Projects.Commands;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : BaseController
    {
        private readonly ILogger<ProjectsController> logger;
        private readonly IMediator mediator;
        private readonly LinkGenerator linkGenerator;

        public ProjectsController(
            IMediator mediator,
            INameGeneratorClient nameGenerator,
            LinkGenerator linkGenerator,
            ILogger<ProjectsController> logger,
            IHttpContextAccessor httpContextAccessor)
            : base(logger, httpContextAccessor, nameGenerator)
        {
            this.logger = Guard.IsNotNull(logger, nameof(logger));
            this.linkGenerator = Guard.IsNotNull(linkGenerator, nameof(linkGenerator));
            this.mediator = Guard.IsNotNull(mediator, nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return new StatusCodeResult(500);
        }

        [HttpGet("{id}", Name = "GetProjectById")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {


            return new StatusCodeResult(500);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateProjectDto project, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(project, nameof(project));
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var command = new CreateProjectCommand(project.ProjectName, this.username, project.StartDate, project.EndDate, Guid.NewGuid());
            try
            {
                await this.mediator.Publish(command);
                return this.Created(new Uri("http://localhost:5500/Project/"), JsonConvert.SerializeObject(project));
            }
            catch (Exception e)
            {
                this.logger.LogError(e, e.Message);
                return new StatusCodeResult(500);
            }
        }
    }

}