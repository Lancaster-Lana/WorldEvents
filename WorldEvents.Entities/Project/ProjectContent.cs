using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldEvents.Entities
{
    public class ProjectContent : FullAuditedEntity<long>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        //public int ProjectId { get; set; }
        //[ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        /// <summary>
        /// Project details: links on sources, etc.
        /// </summary>
        public virtual string Content { get; set; }

        public virtual long NumOfView { get; set; }

        //related, linked project documnents, etc
        //public virtual IList<Project> SubProjects { get; set; }  = new List<Project>();    
    }
}