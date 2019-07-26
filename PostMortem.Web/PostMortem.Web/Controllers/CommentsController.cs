using System.Linq;
using Polly;
using PostMortem.Domain.Comments;
using PostMortem.Domain.Comments.Commands;
using PostMortem.Domain.Comments.Queries;
using PostMortem.Domain.Events.Comments;
using PostMortem.Domain.Projects.Commands;
using PostMortem.Domain.Questions.Queries;
using PostMortem.Infrastructure;
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
    public class CommentsController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly LinkGenerator linkGenerator;
        private readonly ILogger<CommentsController> logger;

        public CommentsController(
            IMediator mediator,
            IMapper mapper,
            LinkGenerator linkGenerator,
            INameGeneratorClient nameGenerator,
            IHttpContextAccessor httpContextAccessor,
            ILogger<CommentsController> logger)
            : base(httpContextAccessor, null)
        {
            this.mapper = Guard.IsNotNull(mapper, nameof(mapper));
            this.mediator = Guard.IsNotNull(mediator, nameof(mediator));
            this.linkGenerator = Guard.IsNotNull(linkGenerator, nameof(linkGenerator));
            this.logger = Guard.IsNotNull(logger, nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
            var result = await this.mediator.Send(new GetAllCommentsQueryArgs());
            if (result.Outcome == OutcomeType.Successful)
            {
                var comments = result.Result?.Select(c => this.mapper.Map<CommentDto>(c));
                return this.Ok(comments);
            }

            logger.LogError(500,$"{result.Outcome} : {result.FaultType}");
            logger.LogDebug(500,result.FinalException,$"{result.Outcome} : {result.FaultType}");
            return new StatusCodeResult(500);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) 
        {
            try
            {
                var result = await this.mediator.Send(new GetCommentByIdQueryArgs {CommentId =id});

                if (result.Outcome == Polly.OutcomeType.Successful)
                {
                    return this.Ok(this.mapper.Map<CommentDto>(result.Result));
                }

                logger.LogError(500,$"{result.Outcome} : {result.FaultType}");
                logger.LogDebug(500,result.FinalException,$"{result.Outcome} : {result.FaultType}");
                return new StatusCodeResult(500);
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

                var result = await this.mediator.Send(new CreateCommentCommandArgs
                {
                    QuestionId = comment.QuestionId,
                    Commenter = comment.Commenter,
                    CommentText = comment.CommentText,
                    DateAdded = comment.DateAdded == DateTime.MinValue ? DateTime.Now : comment.DateAdded,
                });

                if (result.Outcome == OutcomeType.Successful)
                {
                    var url = this.linkGenerator.GetPathByAction(
                        this.HttpContext,
                        controller: "Comments",
                        action: "GetById",
                        values: new { id = comment.CommentId });

                    return this.Created($"{this.HttpContext.Request.Scheme}//{this.HttpContext.Request.Host}{url}", comment);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await this.mediator.Send(new DeleteCommentCommandArgs{CommentId = id});
            if (result.Outcome == OutcomeType.Successful)
            {
                return this.Ok();
            }

            logger.LogError(500,$"{result.Outcome} : {result.ExceptionType}");
            logger.LogDebug(500,result.FinalException,$"{result.Outcome} : {result.ExceptionType}");
            return new StatusCodeResult(500);
        }

        [HttpPost("{id}/likes")]
        public async Task<IActionResult> UpdateLikes([FromRoute]Guid id,[FromBody] LikeCommentDto likes) 
        {
            try
            {
                var result = await this.mediator.Send(new LikeCommentCommandArgs {CommentId =id });

                if (result.Outcome == Polly.OutcomeType.Successful)
                {
                    return this.Ok();
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

        [HttpPost("{id}/dislikes")]
        public async Task<IActionResult> UpdateDislikes([FromRoute]Guid id,[FromBody] DislikeCommentDto dislikes) 
        {
            try
            {
                var result = await this.mediator.Send(new DislikeCommentCommandArgs {CommentId =id });

                if (result.Outcome == Polly.OutcomeType.Successful)
                {
                    return this.Ok();
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateComment([FromRoute]Guid id, [FromBody] UpdateCommentDto dto)
        {
            try
            {
                var result = await this.mediator.Send(new UpdateCommentCommandArgs { CommentId =id, CommentText = dto.CommentText});

                if (result.Outcome == Polly.OutcomeType.Successful)
                {
                    return this.Ok();
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
    }
}