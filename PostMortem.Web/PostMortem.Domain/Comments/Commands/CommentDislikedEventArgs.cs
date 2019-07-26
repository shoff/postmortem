using System;
using MediatR;
using Polly;
using PostMortem.Infrastructure.EventSourcing.Commands;

namespace PostMortem.Domain.Comments
{
    public class DislikeCommentCommandArgs : CommandArgsBase
    {
        public Guid CommentId { get; set; }
    }
}