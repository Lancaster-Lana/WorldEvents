using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorldEvents.Categories.Dto;
using WorldEvents.Entities;
using WorldEvents.Users;

namespace WorldEvents.Application.Dto
{
    [AutoMap(typeof(ApplicationUser))]
    public class ApplicationUserDto : FullAuditedEntityDto<Guid>
    {
        public UserProfileDto UserProfile { get; set; }

        //TODO: !!!
        public string RoleId { get; set; }

        public string Email { get; set; }

        //public virtual ApplicationRoleDto Role { get; set; }

        public ICollection<CategorySubscriptionDto> Subscriptions { get; set; } = new List<CategorySubscriptionDto>();
    }
}