using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.UI;

namespace WorldEvents.Entities
{
    [Table("AppEvents")]
    public class Event:  FullAuditedEntity<Guid>, IMustHaveTenant
    {
        public virtual int TenantId { get; set; } = 1;// TODO: default tenant

        [Range(0, int.MaxValue)]
        public virtual int MaxRegistrationCount { get; set; }

        public string EventType { get; set; }

        public const int MaxTitleLength = 128;
        [Required]
        [StringLength(MaxTitleLength)]
        public string Title { get;  set; }

        public const int MaxDescriptionLength = 2048;
        [StringLength(MaxDescriptionLength)]
        public string Description { get;  set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsCancelled { get;  set; }

        /// <summary>
        ///The list of who is registered to the event
        /// </summary>
        [ForeignKey("EventId")]
        public virtual ICollection<EventRegistration> Registrations { get; set; } = new List<EventRegistration>();
 
        public bool IsInPast
        {
            get
            {
                return StartDate < DateTime.Now;
            }
        }

        public bool IsAllowedCancellationTimeEnded
        {
            get
            {
                return StartDate.Subtract(DateTime.Now).Hours <= 2.0; //2 hours can be defined as Event property and determined per event
            }
        }

        public void AssertNotInPast()
        {
            if (IsInPast)
            {
                throw new UserFriendlyException("This event was in the past");
            }
        }

        public void AssertNotCancelled()
        {
            if (IsCancelled)
            {
                throw new UserFriendlyException("This event is canceled!");
            }
        }
    }
}