namespace PostMortem.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Domain;
    using Domain.Questions;
    using Dtos;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Logging;

    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : BaseController
    {
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
            ILogger<QuestionsController> logger)
            : base(httpContextAccessor, nameGenerator)
        {
            this.mediator = Guard.IsNotNull(mediator, nameof(mediator));
            this.mapper = Guard.IsNotNull(mapper, nameof(mapper));
            this.linkGenerator = Guard.IsNotNull(linkGenerator, nameof(linkGenerator));
            this.logger = Guard.IsNotNull(logger, nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            throw new NotImplementedException();
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
                var result = await this.mediator.Send(Question.CreateQuestionAddedEventArgs(question));
                if (result.Outcome == Polly.OutcomeType.Successful)
                {
                    var url = this.linkGenerator.GetPathByAction(
                        this.HttpContext,
                        controller: "Questions",
                        action: "GetById",
                        values: new { id = question.QuestionId });

                    return this.Created($"{this.HttpContext.Request.Scheme}//{this.HttpContext.Request.Host}{url}", question);
                }

                return new StatusCodeResult(500);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, e.Message);
                return new StatusCodeResult(500);
            }

        }
    }
}