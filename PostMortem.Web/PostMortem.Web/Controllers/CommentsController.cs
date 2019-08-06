namespace PostMortem.Web.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Domain.Comments.Queries;
    using Domain.Voters;
    using Dtos;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Zatoichi.Common.Infrastructure.Extensions;
    using Zatoichi.Common.Infrastructure.Services;

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

        [HttpGet("{questionId}/{id}")]
        public async Task<IActionResult> GetById(Guid questionId, Guid id)
        {
            try
            {
                var query = new GetCommentByIdQuery(questionId, id);
                var result = await this.mediator.Send(query).ConfigureAwait(false);
                return new ApiResult<CommentDto>(
                        HttpStatusCode.OK, this.mapper.Map<CommentDto>(result)).ToActionResult();
                    
            }
            catch (Exception e)
            {
                this.logger.LogError(e, e.Message);
                return new StatusCodeResult(500);
            }
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

                // await this.mediator.Publish(this.eventFactory.CreateEvent(comment.CommentId));
                // this.repository.AddCommentAsync(comment).ConfigureAwait(false);

                //if (result.Outcome == Polly.OutcomeType.Successful)
                //{
                //    var url = this.linkGenerator.GetPathByAction(
                //        this.HttpContext,
                //        controller: "Questions",
                //        action: "GetById",
                //        values: new { id = comment.CommentId });

                //    return this.Created($"{this.HttpContext.Request.Scheme}//{this.HttpContext.Request.Host}{url}", comment);
                //}
                // TODO fix this
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