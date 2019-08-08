namespace PostMortem.Web.Controllers
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Infrastructure;
    using Infrastructure.Questions.Commands;
    using Infrastructure.Questions.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Dtos;
    using Zatoichi.Common.Infrastructure.Extensions;
    using Zatoichi.Common.Infrastructure.Services;

    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : BaseController
    {
        private readonly IMediator mediator;
        private readonly ILogger<QuestionsController> logger;

        public QuestionsController(
            IMediator mediator,
            INameGeneratorClient nameGenerator,
            IHttpContextAccessor httpContextAccessor,
            ILogger<QuestionsController> logger)
            : base(httpContextAccessor, nameGenerator)
        {
            this.mediator = Guard.IsNotNull(mediator, nameof(mediator));
            this.logger = Guard.IsNotNull(logger, nameof(logger));
        }

        [HttpGet("{id}", Name = "GetQuestionById")]
        public async Task<IActionResult> GetQuestionById(Guid id, CancellationToken cancellationToken)
        {
            // this is not exactly how I would like it
            var request = new GetQuestionByIdQuery(id);
            try
            {
                var result = await this.mediator.Send(request, cancellationToken).ConfigureAwait(false);
                return this.Ok(result);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, e.Message);
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QuestionDto question, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(question, nameof(question));

            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            try
            {
                var command = new AddQuestionCommand(question.ProjectId, question.QuestionText, this.voter.VoterId.Id);
                await this.mediator.Publish(command, cancellationToken);
                string id = command.QuestionId == null ? Guid.Empty.ToString() : command.QuestionId.Id.ToString();
                string url = $"https://localhost:5500/api/question/GetQuestionById?id={id}";
                return new CreatedResult(new Uri(url), id);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, e.Message);
                return new StatusCodeResult(500);
            }

        }
    }
}