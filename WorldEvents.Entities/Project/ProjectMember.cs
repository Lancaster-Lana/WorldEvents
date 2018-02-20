
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldEvents.Entities
{
    /// <summary>
    /// Participant of the project.
    /// For example, in one project can be PM, in another - QA
    /// </summary>
    public class ProjectMember : CreationAuditedEntity<long>
    {
        public virtual long? ProjectRoleId { get; set; }
        /// <summary>
        /// Role of the user in specific project 
        /// TODO: may have several roles
        /// </summary>
        [ForeignKey("ProjectRoleId")]
        public virtual ProjectRole ProjectRole { get; set; }

        public virtual string UserId { get; set; } //TODO: long
        [ForeignKey("UserId")] //[ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}