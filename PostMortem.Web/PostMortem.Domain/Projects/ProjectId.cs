using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using PostMortem.Infrastructure;

namespace PostMortem.Domain.Projects
{
    public class ProjectId : EntityId<Guid>
    {
        private const string Prefix = "Project-";
        public ProjectId(Guid id) : base(id)
        {
        }

        public ProjectId(string idString) : this(ExtractGuid(idString))
        {

        }
        private static Guid ExtractGuid(string idString) =>  Guid.Parse(idString.StartsWith(Prefix) ? idString.Substring(Prefix.Length) : idString);

        public override string AsIdString() => $"{Prefix}{Id}";
        public static readonly ProjectId Empty=new ProjectId(Guid.Empty);
        public static ProjectId NewProjectId() => new ProjectId(Guid.NewGuid());
        public static implicit operator ProjectId(Guid id) => new ProjectId(id);
        public static implicit operator ProjectId(string id) => new ProjectId(id);
    }
}
