namespace PostMortem.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Domain;
    using Dtos;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Zatoichi.Common.Infrastructure.Extensions;

    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : BaseController
    {
        private readonly ILogger<CommentsController> logger;
        private readonly IRepository repository;

        public CommentsController(
            INameGeneratorClient nameGenerator,
            IHttpContextAccessor httpContextAccessor,
            ILogger<CommentsController> logger,
            IRepository repository)
            : base(httpContextAccessor, nameGenerator)
        {
            this.logger = logger;
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CommentDto comment)
        {
            Guard.IsNotNull(comment, nameof(comment));
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            try
            {
                if (string.IsNullOrWhiteSpace(comment.Commenter))
                {
                    comment.Commenter = this.username; // generated anonymous username
                }

                var result = await this.repository.AddCommentAsync(comment).ConfigureAwait(false);

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