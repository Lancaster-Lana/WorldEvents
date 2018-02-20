using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace WorldEvents.Entities
{
    [Table("AppEvents")]
    public class Event:  FullAuditedEntity<Guid>, IMustHaveTenant //Entity<Guid>
    {
        public virtual int TenantId { get; set; }

        public const int MaxTitleLength = 128;
        public const int MaxDescriptionLength = 2048;

        [Required]
        [StringLength(MaxTitleLength)]
        public virtual string Title { get;  set; }

        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get;  set; }

        public virtual DateTime Date { get; set; }

        public virtual bool IsCancelled { get;  set; }

        [Range(0, int.MaxValue)]
        public virtual int MaxRegistrationCount { get; set; }

        [ForeignKey("EventId")]
        public virtual ICollection<EventRegistration> Registrations { get; set; }

        public bool IsInPast
        {
            get
            {
                return Date < DateTime.Now;
            }
        }

        public bool IsAllowedCancellationTimeEnded
        {
            get
            {
                return Date.Subtract(DateTime.Now).Hours <= 2.0; //2 hours can be defined as Event property and determined per event
            }
        }

        //public static Event Create(int tenantId, string title, DateTime date, string description = null, int maxRegistrationCount = 0)
        //{
        //    var @event = new Event
        //    {
        //        Id = Guid.NewGuid(),
        //        TenantId = tenantId,
        //        Title = title,
        //        Description = description,
        //        MaxRegistrationCount = maxRegistrationCount
        //    };

        //    @event.SetDate(date);

        //    @event.Registrations = new Collection<EventRegistration>();

        //    return @event;
        //}

        //public void ChangeDate(DateTime date)
        //{
        //    if (date == Date)
        //    {
        //        return;
        //    }

        //    SetDate(date);

        //    DomainEvents.EventBus.Trigger(new EventDateChangedEvent(this));
        //}

        //internal void Cancel()
        //{
        //    AssertNotInPast();
        //    IsCancelled = true;
        //}

        //private void SetDate(DateTime date)
        //{
        //    AssertNotCancelled();

        //    if (date < Clock.Now)
        //    {
        //        throw new UserFriendlyException("Can not set an event's date in the past!");
        //    }

        //    if (date <= Clock.Now.AddHours(3)) //3 can be configurable per tenant
        //    {
        //        throw new UserFriendlyException("Should set an event's date 3 hours before at least!");
        //    }

        //    Date = date;
        //}

        //private void AssertNotInPast()
        //{
        //    if (IsInPast())
        //    {
        //        throw new UserFriendlyException("This event was in the past");
        //    }
        //}

        //private void AssertNotCancelled()
        //{
        //    if (IsCancelled)
        //    {
        //        throw new UserFriendlyException("This event is canceled!");
        //    }
        //}
    }
}