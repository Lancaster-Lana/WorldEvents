using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using Abp.Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WorldEvents.Application.Dto;
using WorldEvents.Core.Events;
using WorldEvents.Entities;
using WorldEvents.Events.Dto;

namespace WorldEvents.ApplicationServices.Events
{
    public class EventAppService : ApplicationService, IEventAppService//AsyncCrudAppService<Event, EventDto, Guid, PagedAndSortedResultRequestDto>
    {
        private readonly IEventManager _eventManager;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public EventAppService(IEventManager eventManager, UserManager<ApplicationUser> userManager, IMapper mapper)// : this(mapper)
        {
            _eventManager = eventManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Event>> GetAllEventsForUserAsync(IIdentity user)
        {
            return _eventManager.GetAllEventsForUserAsync(user.Name);
        }

        public async Task<EventRegistration> RegisterUser(EventDto @event, ApplicationUserDto user)
        {
            var ev = _mapper.Map<Event>(@event);  //@event.MapTo<Event>();
            var us = _mapper.Map<ApplicationUser>(user);  //user.MapTo<ApplicationUser>();

            var registration = await _eventManager.RegisterParticipantAsync(ev, us);

            await CurrentUnitOfWork.SaveChangesAsync();
            return registration;
        }

        public async Task<bool> Create(EventDto @event, string userName)//ApplicationUserDto user)
        {
            var entityEvent = _mapper.Map<Event>(@event); //or Mapper.Map<Event>(@event) //@event.MapTo<Event>(); //- Abp.AutoMapper

            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                //create registration for the user
                var registerUser = EventRegistration.Create(entityEvent, user);
                entityEvent.Registrations.Add(registerUser);
            }

            await _eventManager.CreateAsync(entityEvent);

            //var unitOfWork = IocManager.Instance.Resolve<IActiveUnitOfWork>();
            //await CurrentUnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(EventDto @event)
        {
            var entityEvent = _mapper.Map<Event>(@event); //or Mapper.Map<Event>(@event) //@event.MapTo<Event>(); //- Abp.AutoMapper

            return _eventManager.Update(entityEvent);
        }

        public async Task<bool> Delete(Guid id)
        {
            Event @event = await _eventManager.GetAsync(id);

            return _eventManager.Cancel(@event);
        }

        public async Task<IEnumerable<EventRegistration>> GetEventRegistrationsAsync(EventDto @event)
        {
            return await _eventManager.GetEventRegistrationsAsync(@event.Id);
        }
    }

    //[AbpAuthorize]
    /*
    public class EventAppService : EventCloudAppServiceBase, IEventAppService
    {
        private readonly IEventManager _eventManager;
        private readonly IRepository<Event, Guid> _eventRepository;

        public EventAppService(
            IEventManager eventManager,
            IRepository<Event, Guid> eventRepository)
        {
            _eventManager = eventManager;
            _eventRepository = eventRepository;
        }

        public async Task<ListResultDto<EventListDto>> GetList(GetEventListInput input)
        {
            var events = await _eventRepository
                .GetAll()
                .Include(e => e.Registrations)
                .WhereIf(!input.IncludeCanceledEvents, e => !e.IsCancelled)
                .OrderByDescending(e => e.CreationTime)
                .Take(64)
                .ToListAsync();

            return new ListResultDto<EventListDto>(events.MapTo<List<EventListDto>>());
        }

        public async Task<EventDetailOutput> GetDetail(EntityDto<Guid> input)
        {
            var @event = await _eventRepository
                .GetAll()
                .Include(e => e.Registrations)
                .Where(e => e.Id == input.Id)
                .FirstOrDefaultAsync();

            if (@event == null)
            {
                throw new UserFriendlyException("Could not found the event, maybe it's deleted.");
            }

            return @event.MapTo<EventDetailOutput>();
        }

        public async Task Create(CreateEventInput input)
        {
            var @event = Event.Create(AbpSession.GetTenantId(), input.Title, input.Date, input.Description, input.MaxRegistrationCount);
            await _eventManager.CreateAsync(@event);
        }

        public async Task Cancel(EntityDto<Guid> input)
        {
            var @event = await _eventManager.GetAsync(input.Id);
            _eventManager.Cancel(@event);
        }

        public async Task<EventRegisterOutput> Register(EntityDto<Guid> input)
        {
            var registration = await RegisterAndSaveAsync(
                await _eventManager.GetAsync(input.Id),
                await GetCurrentUserAsync()
                );

            return new EventRegisterOutput
            {
                RegistrationId = registration.Id
            };
        }

        public async Task CancelRegistration(EntityDto<Guid> input)
        {
            await _eventManager.CancelRegistrationAsync(
                await _eventManager.GetAsync(input.Id),
                await GetCurrentUserAsync());
        }

        private async Task<EventRegistration> RegisterAndSaveAsync(Event @event, User user)
        {
            var registration = await _eventManager.RegisterAsync(@event, user);
            await CurrentUnitOfWork.SaveChangesAsync();
            return registration;
        }
    }
    */
}
