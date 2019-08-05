﻿namespace PostMortem.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Domain.Voters;
    using Dtos;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : BaseController
    {
        private readonly ILogger<CommentsController> logger;

        public CommentsController(
            
            INameGeneratorClient nameGenerator,
            IHttpContextAccessor httpContextAccessor,
            ILogger<CommentsController> logger)
            : base(httpContextAccessor, nameGenerator)
        {
            this.logger = Guard.IsNotNull(logger, nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                //var result = await this.mediator.Send<(this.mapper.Map<CommentGetByIdEvent>(id));
                //if (result.Outcome == Polly.OutcomeType.Successful)
                //{
                //    return this.Ok(this.mapper.Map<CommentDto>(result.Result));
                //}

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