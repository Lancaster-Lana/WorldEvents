using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Services;
using WorldEvents.Entities;
using WorldEvents.Users;

namespace WorldEvents.Core.Events
{
    public interface IEventManager : IDomainService
    {
        Task<Event> GetAsync(Guid id);

        Task<Event> CreateAsync(Event @event);

        //Task<bool> UpdateAsync(Event @event);
        bool Update(Event @event);

        bool Cancel(Event @event);

        Task<EventRegistration> RegisterParticipantAsync(Event @event, ApplicationUser user);

        bool CancelRegistration(Event @event, ApplicationUser user);

        List<ApplicationUser> GetRegisteredUsersAsync(Event @event);

        IEnumerable<Event> GetAllEventsForUserAsync(string userName);


        Task<IEnumerable<EventRegistration>> GetEventRegistrationsAsync(Guid id);

        //Task<IReadOnlyList<User>> GetRegisteredUsersAsync(Event @event);
    }
}