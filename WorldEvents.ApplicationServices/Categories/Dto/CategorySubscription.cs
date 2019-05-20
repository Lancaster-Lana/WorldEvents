using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorldEvents.Entities;

namespace WorldEvents.Categories.Dto
{
    [AutoMap(typeof(CategorySubscription))]
    public class CategorySubscriptionDto : FullAuditedEntityDto<long>
    {
        public virtual long CategoryId { get; set; }
        //public virtual CategoryDto Category { get; set; }

        public string UserId { get; set; } //TODO: long
        //public virtual ApplicationUserDto User { get; set; }
        //public virtual string UserName { get; set; }
    }
}
