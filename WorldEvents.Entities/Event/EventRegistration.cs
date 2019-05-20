using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace WorldEvents.Entities
{
    [Table("AppEventRegistrations")]
    public class EventRegistration : FullAuditedEntity<int>, IMustHaveTenant
    {
        public virtual int TenantId { get; set; } = 1; //TODO" default 

        public virtual Guid EventId { get; set; }
        //[ForeignKey("EventId")]
        public virtual Event Event { get; set; }

        public virtual string UserId { get; set; }
        //[ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public static EventRegistration Create(Event @event, ApplicationUser user)//, IEventRegistrationPolicy registrationPolicy)
        {
            if (@event == null) { throw new ArgumentNullException("event"); }
            if (user == null) { throw new ArgumentNullException("user"); }
            //if (registrationPolicy == null) { throw new ArgumentNullException("registrationPolicy"); }

            //await registrationPolicy.CheckRegistrationAttemptAsync(@event, user);

            return new EventRegistration
            {
                CreationTime = DateTime.Now,
                CreatorUserId = 1, // TODO: must be current user
                TenantId = @event.TenantId,// TODO: default tenant
                EventId = @event.Id,
                //Event = @event,
                UserId = @user.Id,
                //User = user
            };
        }
    }
}