using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorldEvents.Application.Dto;
using WorldEvents.Events.Dto;

namespace WorldEvents.Models
{
    public class EventModel
    {
        /// <summary>
        /// For proper JSON serialization
        /// </summary>
        public EventModel() { }

        public virtual int TenantId { get; set; }

        public const int MaxTitleLength = 128;
        public const int MaxDescriptionLength = 2048;

        /// <summary>
        /// Enum: Conference, Skype call
        /// </summary>
        public string EventType { get; set; }

        [Required]
        [StringLength(MaxTitleLength)]
        public string Title { get; set; }

        [StringLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        //[DatesValidator(StartDate, ErrorMessage = "The end date must be greater or equal to start date")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// All participants that can be selected
        /// </summary>
        public IList<ApplicationUserDto> AllUsers { get; set; }

        ///// <summary>
        ///// Registered to event users
        ///// </summary>
        //public List<ApplicationUserDto> Participants { get; set; }

        public ICollection<EventRegistrationDto> Registrations { get; set; }

        public bool IsCancelled { get; set; }

        [Range(0, int.MaxValue)]
        public int MaxRegistrationCount { get; set; }

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
                return StartDate?.Subtract(DateTime.Now).Hours <= 2.0; //2 hours can be defined as Event property and determined per event
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
