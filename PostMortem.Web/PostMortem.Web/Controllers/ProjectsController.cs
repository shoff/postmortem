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
            //var result = await this.mediator.Send(Project.CreateGetByIdEventArgs(new ProjectId(id)));
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
            var p = new Project
            {
                ProjectId = new ProjectId(Guid.NewGuid()),
                EndDate = project.EndDate,
                ProjectName = project.ProjectName,
                StartDate = project.StartDate
            };
            //var result = await this.mediator.Send(Project.CreateProjectCreatedEventArgs(p));

            //if (result.Outcome == OutcomeType.Successful)
            //{
            //    var url = this.linkGenerator.GetPathByAction(
            //        this.HttpContext,
            //        controller: "Projects",
            //        action: "GetById",
            //        values: new {id = p.ProjectId});

            //    return this.Created($"{this.HttpContext.Request.Scheme}//{this.HttpContext.Request.Host}{url}", this.mapper.Map<Project>(p));
            //}

            return new StatusCodeResult(500);
        }
    }

}