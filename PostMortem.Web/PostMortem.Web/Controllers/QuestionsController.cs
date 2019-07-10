namespace PostMortem.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Data;
    using Dtos;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Services;
    using Zatoichi.Common.Infrastructure.Extensions;

    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : BaseController
    {
        private readonly ILogger<QuestionsController> logger;
        private readonly IRepository repository;

        public QuestionsController(
            INameGeneratorClient nameGenerator,
            IHttpContextAccessor httpContextAccessor,
            ILogger<QuestionsController> logger,
            IRepository repository)
            : base(httpContextAccessor, nameGenerator)
        {
            this.logger = logger;
            this.repository = Guard.IsNotNull(repository, nameof(repository));
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
                return result.ToActionResult();
            }
            catch (Exception e)
            {
                this.logger.LogError(e, e.Message);
                return new StatusCodeResult(500);
            }

        }
    }
}