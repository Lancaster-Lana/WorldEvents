using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldEvents.Entities
{
    /// <summary>
    /// Category News
    /// </summary>
    public class News : FullAuditedEntity<long>
    {
        //[Required]
        public long? CategoryId { get; set; }
        //[ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        //[Required]
        public long? NewsContentId { get; set; }
        [ForeignKey("NewsContentId")]
        public virtual NewsContent NewsContent { get; set; }
    }
}