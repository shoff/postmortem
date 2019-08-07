namespace PostMortem.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Domain.Voters;
    using Dtos;
    using Infrastructure.Projects.Commands;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : BaseController
    {
        private readonly IMediator mediator;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ProjectsController(
            IMediator mediator,
            IMapper mapper,
            INameGeneratorClient nameGenerator,
            LinkGenerator linkGenerator,
            IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor, nameGenerator)
        {
            this.mediator = Guard.IsNotNull(mediator, nameof(mediator));
            this.linkGenerator = Guard.IsNotNull(linkGenerator, nameof(linkGenerator));
            this.mapper = Guard.IsNotNull(mapper, nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var result = await this.mediator.Send(Project.CreateGetAllEventArgs());
            //if (result.Outcome == OutcomeType.Successful)
            //{
            //    var projects = result.Result.Map(p => this.mapper.Map<ProjectDto>(p));
            //    return this.Ok(projects);
            //}

            return new StatusCodeResult(500);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            //var result = await this.mediator.Send(Project.CreateGetByIdEventArgs(id));
            //if (result.Outcome == OutcomeType.Successful)
            //{
            //    var project = this.mapper.Map<ProjectDto>(result.Result);
            //    return this.Ok(project);
            //}

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
            var p = new CreateProjectCommand(project.ProjectName, project.StartDate, project.EndDate, Guid.NewGuid());
            await this.mediator.Publish(p);
            return new StatusCodeResult(500);
        }
    }

}