namespace PostMortem.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Domain;
    using Dtos;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Logging;

    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : BaseController
    {
        private readonly LinkGenerator linkGenerator;
        private readonly ILogger<QuestionsController> logger;
        private readonly IRepository repository;

        public QuestionsController(
            LinkGenerator linkGenerator,
            INameGeneratorClient nameGenerator,
            IHttpContextAccessor httpContextAccessor,
            ILogger<QuestionsController> logger,
            IRepository repository)
            : base(httpContextAccessor, nameGenerator)
        {
            this.linkGenerator = Guard.IsNotNull(linkGenerator, nameof(linkGenerator));
            this.logger = Guard.IsNotNull(logger, nameof(logger));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
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
                var result = await this.repository.AddQuestionAsync(question).ConfigureAwait(false);
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