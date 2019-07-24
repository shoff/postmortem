using MediatR;
using Polly;
using PostMortem.Domain.Comments;
using PostMortem.Domain.EventSourcing.Commands;

namespace PostMortem.Domain.Events.Comments
{
    using System;

    public class LikeCommentCommandArgs : CommandArgsBase
    {
        public Guid CommentId { get; set; }
    }

}