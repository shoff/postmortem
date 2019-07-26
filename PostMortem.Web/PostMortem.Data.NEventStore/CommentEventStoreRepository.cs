using System;
using Microsoft.Extensions.Logging;
using NEventStore;
using PostMortem.Domain.Comments;
using PostMortem.Domain.Comments.Events;

namespace PostMortem.Data.NEventStore
{
    public class CommentEventStoreRepository : NEventStoreRepository<Comment, CommentId, CommentEventArgsBase>,
        ICommentEventStoreRepository
    {
        public CommentEventStoreRepository(IStoreEvents eventStore, ILogger<CommentEventStoreRepository> logger) : base(
            eventStore, logger)
        {

        }

        protected override string BucketName => "comment_events";

    }
}