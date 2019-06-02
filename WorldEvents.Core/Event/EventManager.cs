using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Events.Bus;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using WorldEvents.Entities;

namespace WorldEvents.Core.Events
{
    /// <summary>
    /// Domain service to manage events logic
    /// </summary>
    public class EventManager : IEventManager
    {
        public IEventBus EventBus { get; set; }

        private readonly IRepository<Event, Guid> _eventRepository;
        private readonly IRepository<EventRegistration, int> _eventRegistrationRepository;

        private readonly IEventRegistrationPolicy _registrationPolicy;

        public EventManager(
            IEventRegistrationPolicy registrationPolicy,
            IRepository<EventRegistration, int> eventRegistrationRepository,
            IRepository<Event, Guid> eventRepository)
        {
            _registrationPolicy = registrationPolicy;
            _eventRegistrationRepository = eventRegistrationRepository;
            _eventRepository = eventRepository;

            EventBus = NullEventBus.Instance; //init default Service Bus
        }

        public async Task<Event> GetAsync(Guid id)
        {
            var @event = _eventRepository.Get(id);
            if (@event == null)
            {
                throw new UserFriendlyException("Could not found the event, maybe it's deleted!");
            }

            return @event;
        }

        public async Task<IQueryable<Event>> GetAsync(params Expression<Func<Event, object>>[] propertySelectors)
        {
            return _eventRepository.GetAllIncluding(propertySelectors);//GetAllIncluding(propertySelectors).ToList();
        }

        public async Task<Event> CreateAsync(Event @event)
        {
            return await _eventRepository.Insert(@event);
        }

        //public async Task<bool> UpdateAsync(Event @event)
        //{
        //    return await _eventRepository.Update(@event);
        //}

        public bool Update(Event @event)
        {
            return _eventRepository.Update(@event);
        }

        /// <summary>
        /// Register user on the event
        /// </summary>
        /// <param name="event"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<EventRegistration> RegisterParticipantAsync(Event @event, ApplicationUser user)
        {
            var registration = EventRegistration.Create(@event, user);//, _registrationPolicy);
            return await _eventRegistrationRepository.Insert(registration);
        }

        public bool Cancel(Event @event)
        {
            //@event.Cancel();
            //Notify registered users about event canceled
            EventBus.Trigger(new EventCancelledEvent(@event));

            //EventBus.Unregister(@event)

            //TODO: Mark event as canceled, not remove
            return _eventRepository.Delete(@event);
        }

        /// <summary>
        /// Cancel user registration 
        /// </summary>
        /// <param name="event"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool CancelRegistration(Event @event, ApplicationUser user)
        {
            var registration = _eventRegistrationRepository.GetAll().FirstOrDefault(r => r.EventId == @event.Id && r.UserId == user.Id.ToString());
            if (registration == null)
            {
                return true; //No need to cancel since there is no such a registration
            }

            bool canceled = _eventRegistrationRepository.Delete(registration); //registration.CancelAsync(_eventRegistrationRepository);

            return canceled;
        }

        public static void ChangeEventDate(Event @event, DateTime startDate)
        {
            if (startDate == @event.StartDate)
            {
                return;
            }

            SetEventDate(@event, startDate);

            //Notify service bus about change
            //DomainEvents.EventBus.Trigger(new EventDateChangedEvent(@event));
        }

        private static void SetEventDate(Event @event, DateTime date)
        {
            @event.AssertNotCancelled();

            if (date < DateTime.Now)
            {
                throw new UserFriendlyException("Can not set an event's date in the past!");
            }

            if (date <= DateTime.Now.AddHours(3)) //3 can be configurable per tenant
            {
                throw new UserFriendlyException("Should set an event's date 3 hours before at least!");
            }

            @event.StartDate = date;
        }

        /// <summary>
        /// Get users registered on the event
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public List<ApplicationUser> GetRegisteredUsersAsync(Event @event)
        {
            var eventRegisteredUsers = @event.Registrations.Where(r=>r.EventId == @event.Id).Select(r => r.User).ToList();
            return eventRegisteredUsers;
        }

        /// <summary>
        /// Get events user registered for
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IEnumerable<Event> GetAllEventsForUserAsync(string userName)
        {
            var eventForUser = _eventRepository.GetAllIncluding(c => c.Registrations)
                .Where(e => e.Registrations.Select(p => p.User.UserName).Contains(userName));

            return eventForUser;
        }


        /// <summary>
        /// Get registered users for that event
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<EventRegistration>> GetEventRegistrationsAsync(Guid id)
        {
            var list = _eventRegistrationRepository.GetAllIncluding(r => r.User).Where(r => r.EventId == id);

            return await list.ToListAsync();
        }

        /*
        public async Task<IReadOnlyList<User>> GetRegisteredUsersAsync(Event @event)
        {
            return await _eventRegistrationRepository
                .GetAll()
                .Include(registration => registration.User)
                .Where(registration => registration.EventId == @event.Id)
                .Select(registration => registration.User)
                .ToListAsync();
        }*/

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
    }
}