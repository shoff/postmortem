namespace PostMortem.Web.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Domain.Voters;
    using Infrastructure;
    using Infrastructure.Questions.Commands;
    using Infrastructure.Questions.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Logging;
    using Dtos;
    using Zatoichi.Common.Infrastructure.Extensions;
    using Zatoichi.Common.Infrastructure.Services;

    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : BaseController
    {
        private readonly IRepository repository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly ILogger<QuestionsController> logger;

        public QuestionsController(
            IMapper mapper,
            IMediator mediator,
            LinkGenerator linkGenerator,
            INameGeneratorClient nameGenerator,
            IHttpContextAccessor httpContextAccessor,
            IRepository repository,
            ILogger<QuestionsController> logger)
            : base(httpContextAccessor, nameGenerator)
        {
            this.repository = Guard.IsNotNull(repository, nameof(repository));
            this.mediator = Guard.IsNotNull(mediator, nameof(mediator));
            this.mapper = Guard.IsNotNull(mapper, nameof(mapper));
            this.linkGenerator = Guard.IsNotNull(linkGenerator, nameof(linkGenerator));
            this.logger = Guard.IsNotNull(logger, nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestionById(Guid id)
        {
            // this is not exactly how I would like it
            var request = new GetQuestionByIdQuery(id);
            var result = await this.mediator.Send(request).ConfigureAwait(false);
            var apiResult = new ApiResult<QuestionDto>(HttpStatusCode.OK, result);
            return apiResult.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QuestionDto question)
        {
            Guard.IsNotNull(question, nameof(question));

            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            try
            {
                var command = new AddQuestionCommand(question.ProjectId, question.QuestionText);
                await this.mediator.Publish(command);

                // TODO 
                return new CreatedResult("", null);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, e.Message);
                return new StatusCodeResult(500);
            }

        }
    }
}