using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorldEvents.Entities
{
    /// <summary>
    /// System roles: admin, registered user
    /// Project roles:
    /// 3.1 Project manager (maybe in several projects)
    /// 3.2 Solution enginer
    /// 3.3 QA
    /// 3.4 Tech writer
    /// 3.5 Designer
    /// </summary>
    public class ProjectRole : CreationAuditedEntity<long>
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        //[ForeignKey("RoleId")]
        public virtual ICollection<CategoryPermission> CategoryPermissions { get; set; } = new List<CategoryPermission>();
    }
}