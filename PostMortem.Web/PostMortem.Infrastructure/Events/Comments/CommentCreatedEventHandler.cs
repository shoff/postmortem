﻿using PostMortem.Domain.Comments;
using PostMortem.Domain.EventSourcing.Events;

namespace PostMortem.Infrastructure.Events.Comments
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Domain;
    using Domain.Events.Comments;
    using MediatR;
    using Polly;
    using Zatoichi.Common.Infrastructure.Resilience;

    //public class CommentCreatedEventHandler : IEventHandler<CommentCreatedEventArgs>
    //{
    //    private readonly IExecutionPolicies executionPolicies;
    //    private readonly ICommentRepository repository;

    //    public CommentCreatedEventHandler(
    //        ICommentRepository repository,
    //        IExecutionPolicies executionPolicies)
    //    {
    //        this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
    //        this.repository = Guard.IsNotNull(repository, nameof(repository));
    //    }

    //    public Task Handle(CommentCreatedEventArgs notification, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}