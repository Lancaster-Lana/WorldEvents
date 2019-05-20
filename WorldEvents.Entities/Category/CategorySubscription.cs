using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldEvents.Entities
{
    public class CategorySubscription : FullAuditedEntity<long>
    {
        //public long CategoryId { get; set; }
        //[ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        //[Column("UserId")]
        public string UserId { get; set; } //TODO: long
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        //public virtual string UserName { get; set; }
    }
}
