using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldEvents.Entities
{
    public class Project : FullAuditedEntity<long>
    {
        //[Required]
        //public string Name { get; set; }

        public long CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public long? CoordinatorId { get; set; }
        [ForeignKey("CoordinatorId")]
        public virtual UserProfile Coordinator { get; set; }

        //public long ProjectContentId { get; set; }
        //[ForeignKey("ProjectId")]
        public virtual ProjectContent ProjectContent { get; set; }

        /// <summary>
        /// Participans of the project (users and volonteers)
        /// </summary>
        [ForeignKey("ProjectId")]
        public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
    }
}
