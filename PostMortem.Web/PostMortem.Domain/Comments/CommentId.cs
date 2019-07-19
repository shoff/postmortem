using System;
using System.Collections.Generic;
using System.Text;

namespace PostMortem.Domain.Comments
{
    public class CommentId : EntityId<Guid>
    {
        private const string Prefix = "Comment-";
        public CommentId(Guid id) : base(id)
        {
        }

        public CommentId(string idString) : this(ExtractGuid(idString))
        {

        }
        private static Guid ExtractGuid(string idString) =>  Guid.Parse(idString.StartsWith(Prefix) ? idString.Substring(Prefix.Length) : idString);

        public override string AsIdString() => $"{Prefix}{Id}";

        public static readonly CommentId Empty=new CommentId(Guid.Empty);
        public static CommentId NewCommentId() => new CommentId(Guid.NewGuid());
    }
}
