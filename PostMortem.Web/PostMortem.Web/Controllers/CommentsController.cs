namespace PostMortem.Web.Controllers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Dtos;
    using Infrastructure;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : BaseController
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly ILogger<CommentsController> logger;

        public CommentsController(
            IMediator mediator,
            IMapper mapper,
            INameGeneratorClient nameGenerator,
            IHttpContextAccessor httpContextAccessor,
            ILogger<CommentsController> logger)
            : base(httpContextAccessor, nameGenerator)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.logger = Guard.IsNotNull(logger, nameof(logger));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CommentDto comment, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(comment, nameof(comment));
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            try
            {

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