namespace PostMortem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Data;
    using Dtos;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Services;
    using Zatoichi.Common.Infrastructure.Extensions;

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : BaseController
    {
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IRepository repository;

        public ProjectsController(
            IMapper mapper,
            INameGeneratorClient nameGenerator,
            LinkGenerator linkGenerator,
            IHttpContextAccessor httpContextAccessor,
            IRepository repository)
            : base(httpContextAccessor, nameGenerator)
        {
            this.linkGenerator = Guard.IsNotNull(linkGenerator, nameof(linkGenerator));
            this.mapper = Guard.IsNotNull(mapper, nameof(mapper));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var projects = await this.repository.GetAllProjectsAsync();
            return this.Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var vm = await this.repository.GetByProjectIdAsync(id);
            if (vm.Item1 == null)
            {
                return this.NotFound(id);
            }
            var projectDto = this.mapper.Map<ProjectDto>(vm.Item1);
            projectDto.Questions.AddRange(vm.Item2.Map(q => this.mapper.Map<QuestionDto>(q)).ToList());
            return this.Ok(projectDto);
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
            var entity = await this.repository.CreateAsync(project);

            var url = this.linkGenerator.GetPathByAction(
                this.HttpContext, 
                controller: "Projects", 
                action: "GetById", 
                values: new { id = entity.ProjectId });

            return this.Created($"{this.HttpContext.Request.Scheme}//{this.HttpContext.Request.Host}{url}", entity);
        }
    }

}