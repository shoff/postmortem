﻿using Polly;
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
        public async Task<IActionResult> Create([FromBody] QuestionDto question)
        {
            Guard.IsNotNull(question, nameof(question));
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (question.ProjectId == Guid.Empty)
            {
                return this.StatusCode(400,new []{$"The ProjectId must be set and not equal to {Guid.Empty}"});
            }

            try
            {
                var questionId = question.QuestionId == Guid.Empty ? Guid.Empty : question.QuestionId;
                var result = await this.mediator.Send(new CreateQuestionCommandArgs{Active = true, QuestionId = questionId, ProjectId = question.ProjectId, Importance = question.Importance, QuestionText = question.QuestionText, LastUpdated = DateTime.Now});
                if (result.Outcome == Polly.OutcomeType.Successful)
                {
                    var url = this.linkGenerator.GetPathByAction(
                        this.HttpContext,
                        controller: "Questions",
                        action: "GetById",
                        values: new { id = question.QuestionId });

                    return this.Created($"{this.HttpContext.Request.Scheme}//{this.HttpContext.Request.Host}{url}", question);
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
                var comments = result.Result.Map(q => this.mapper.Map<CommentDto>(q));
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