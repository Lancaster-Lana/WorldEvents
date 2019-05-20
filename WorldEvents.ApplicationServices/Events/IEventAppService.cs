using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using WorldEvents.Application.Dto;
using WorldEvents.Entities;
using WorldEvents.Events.Dto;

namespace WorldEvents.ApplicationServices.Events
{
    public interface IEventAppService : IApplicationService//IAsyncCrudAppService<EventDto, Guid, PagedAndSortedResultRequestDto>
    {
        //EventDto
        Task<IEnumerable<Event>> GetAllEventsForUserAsync(IIdentity user);

        /// <summary>
        /// Register participant on the event(user must be registerd in the system)
        /// </summary>
        /// <param name="event"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        Task<EventRegistration> RegisterUser(EventDto @event, ApplicationUserDto user);

        /// <summary>
        /// Create\register event
        /// </summary>
        /// <param name="event"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> Create(EventDto @event, string userName);// ApplicationUserDto user);

        /// <summary>
        /// Update event information
        /// NOTE: notifications should be send
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        Task<bool> Update(EventDto @event);

        Task<bool> Delete(Guid id);
    }
}