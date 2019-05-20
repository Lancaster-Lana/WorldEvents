using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace WorldEvents.Events.Dto
{
    //[AutoMap(typeof(Event))]
    public class EventDto : FullAuditedEntityDto<Guid>
    {
        public int TenantId { get; set; } = 0; //TODO: customer, organisation

        [Range(0, int.MaxValue)]
        public int MaxRegistrationCount { get; set; }

        //Enum
        public string EventType { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsCancelled { get; set; }

        public ICollection<EventRegistrationDto> Registrations { get; set; }

        //public ICollection<ApplicationUserDto> Participants { get; set; } = new List<ApplicationUserDto>();

        public EventDto()
        {
        }
    }
}