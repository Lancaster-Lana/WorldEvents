using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using WorldEvents.Application.Dto;

namespace WorldEvents.Events.Dto
{
    public class EventRegistrationDto : FullAuditedEntityDto<int>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public Guid EventId { get; set; }

        public EventDto Event { get; set; }

        public string UserId { get; set; }
        public ApplicationUserDto User { get; set; }
    }
}