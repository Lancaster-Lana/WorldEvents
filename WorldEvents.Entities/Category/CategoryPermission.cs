using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldEvents.Entities
{
    /// <summary>
    /// Permission to access the category. For example: read project, but not edit
    /// </summary>
    public class CategoryPermission : FullAuditedEntity<long>
    {
        /// <summary>
        /// If set, this permission can be granted only if parent is granted.
        /// </summary>
        //public Permission Parent { get; set; }

        /// <summary>
        /// Unique name of the permission. This is the key name to grant permissions.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// This can be used to show permission to the user.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Detail description for this permission.
        /// </summary>
        public string Description { get; set; }

        public virtual long CategoryId { get; set; }
        //[ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual bool ReadOnly { get; set; }
        public virtual bool Edit { get; set; }
        public virtual bool FullControl
        {
            get
            {
                return ReadOnly & Edit;
            }
        }

        // Which side can use this permission.
        //public MultiTenancySides MultiTenancySides { get; set; }

        // Depended feature(s) of this permission.
        //public IFeatureDependency FeatureDependency { get; set; }

        //     List of child permissions. A child permission can be granted only if parent is granted.
       // public IReadOnlyList<Permission> Children { get; }
    }
}
