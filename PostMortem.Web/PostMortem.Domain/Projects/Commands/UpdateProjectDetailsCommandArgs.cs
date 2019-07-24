using System;
using PostMortem.Domain.EventSourcing.Commands;

namespace PostMortem.Domain.Projects.Commands
{
    public class UpdateProjectDetailsCommandArgs : CommandArgsBase
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}