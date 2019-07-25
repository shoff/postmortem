using System.Linq;
using Polly;
using PostMortem.Domain.Comments.Queries;
using PostMortem.Domain.Projects;
using PostMortem.Domain.Questions.Commands;
using PostMortem.Domain.Questions.Queries;
using Zatoichi.Common.Infrastructure.Extensions;

namespace PostMortem.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Domain;
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
            : base(httpContextAccessor, null)
        {
            this.mediator = Guard.IsNotNull(mediator, nameof(mediator));
            this.mapper = Guard.IsNotNull(mapper, nameof(mapper));
            this.linkGenerator = Guard.IsNotNull(linkGenerator, nameof(linkGenerator));
            this.logger = Guard.IsNotNull(logger, nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
            var result = await this.mediator.Send(new GetAllQuestionsQueryArgs());
            if (result.Outcome == OutcomeType.Successful)
            {
                var questions = result.Result?.Select(q => this.mapper.Map<QuestionDto>(q)) ?? new QuestionDto[]{};
                return this.Ok(questions);
            }

            logger.LogError(500,$"{result.Outcome} : {result.FaultType}");
            logger.LogDebug(500,result.FinalException,$"{result.Outcome} : {result.FaultType}");
            return new StatusCodeResult(500);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await this.mediator.Send(new GetQuestionByIdQueryArgs{QuestionId = id});
            if (result.Outcome == OutcomeType.Successful)
            {
                var question = this.mapper.Map<QuestionDto>(result.Result);
                return this.Ok(question);
            }

            logger.LogError(500,$"{result.Outcome} : {result.FaultType}");
            logger.LogDebug(500,result.FinalException,$"{result.Outcome} : {result.FaultType}");
            return new StatusCodeResult(500);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QuestionDto apiQuestion)
        {
            Guard.IsNotNull(apiQuestion, nameof(apiQuestion));
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (apiQuestion.ProjectId == Guid.Empty) // TODO: Convert to a validation attribute.
            {
                return this.StatusCode(400,new []{$"The ProjectId must be set and not equal to {Guid.Empty}"});
            }

            try
            {
                apiQuestion.QuestionId = apiQuestion.QuestionId == Guid.Empty ? Guid.NewGuid() : apiQuestion.QuestionId;
                var result = await this.mediator.Send(new CreateQuestionCommandArgs{Active = true, QuestionId = apiQuestion.QuestionId, ProjectId = apiQuestion.ProjectId, Importance = apiQuestion.Importance, QuestionText = apiQuestion.QuestionText, LastUpdated = DateTime.Now});
                if (result.Outcome == Polly.OutcomeType.Successful)
                {
                    var url = this.linkGenerator.GetPathByAction(
                        this.HttpContext,
                        controller: "Questions",
                        action: "GetById",
                        values: new { id = apiQuestion.QuestionId });

                    return this.Created($"{this.HttpContext.Request.Scheme}//{this.HttpContext.Request.Host}{url}", apiQuestion);
                }

                logger.LogError(500,$"{result.Outcome} : {result.ExceptionType}");
                logger.LogDebug(500,result.FinalException,$"{result.Outcome} : {result.ExceptionType}");
                return new StatusCodeResult(500);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, e.Message);
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("{questionId}/comments")]
        public async Task<IActionResult> GetCommentsForQuestion(Guid questionId)
        {
            var result = await this.mediator.Send(new GetCommentsForQuestionQueryArgs{QuestionId = questionId});
            if (result.Outcome == OutcomeType.Successful)
            {
                var comments = result.Result?.Select(c => this.mapper.Map<CommentDto>(c)) ?? new CommentDto[]{};
                return this.Ok(comments);
            }

            logger.LogError(500,$"{result.Outcome} : {result.FaultType}");
            logger.LogDebug(500,result.FinalException,$"{result.Outcome} : {result.FaultType}");
            return new StatusCodeResult(500);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await this.mediator.Send(new DeleteQuestionCommandArgs{QuestionId = id});
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