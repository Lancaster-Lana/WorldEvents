using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WorldEvents.Application.Dto;
using WorldEvents.ApplicationServices.Events;
using WorldEvents.Entities;
using WorldEvents.Events;
using WorldEvents.Events.Dto;
using WorldEvents.Models;

namespace WorldEvents.Controllers
{
    public class JavaScriptResult : ContentResult
    {
        public JavaScriptResult(string script)
        {
            this.Content = script;
            this.ContentType = "application/javascript";
        }
    }

    //[Produces("application/json")]
    public class EventController : Controller
    {
        public readonly IEventAppService _eventService;
        public readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public EventController(IEventAppService eventService, 
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _eventService = eventService;
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Get th user events (he\she is registerd) list
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> GetCalendarData()
        {
            var eventDBList = (await _eventService.GetAllEventsForUserAsync(User.Identity)).ToList();
            var list = JsonConvert.SerializeObject(eventDBList,
                    Formatting.None,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

            return Content(list, "application/json");

            //var list = JsonConvert.SerializeObject(eventDBList.ToList());
            //return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Event details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //[HttpPost]
        public async Task<IActionResult> Details(EventDto model)
        {
            model.Description = model.Description ?? model.Title; //TODO:
            var eventRegistrations = _mapper.Map<IEnumerable<EventRegistrationDto>> ( await _eventService.GetEventRegistrationsAsync(model));
            model.Registrations = eventRegistrations.ToList();// new List<EventRegistrationDto>();
            //model.AllUsers = Users.GetAll();
            return PartialView(model);
        }

        /// <summary>
        /// Create event
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(EventModel model)//string eventType, DateTime? startDay, DateTime? endDate)
        {
            model.Description = model.Title; //TODO:
            var sysUsers = _userManager.Users.Include(u => u.UserProfile).ToList();
            model.AllUsers = _mapper.Map<List<ApplicationUserDto>>(sysUsers);

            return PartialView(model);
        }

        /// <summary>
        /// Create user on event (update dropped title, time, paticipants)
        /// and register the current user on it (or a list of added user)
        /// </summary>
        /// <param name="model">event data from rest api call</param>
        [HttpPost]
        public async Task<ActionResult> Register(EventDto model)//string eventType, DateTime? startDay, DateTime? endDate)
        {
            if (!User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError("", "You are not authenticated, so cannot create event");
                return View();
            }

            //Add event and current user as participant 
            bool eventCreated = await _eventService.Create(model, User.Identity.Name);//RegisterUser(model, User); //add to DB

            if (eventCreated)
            {
                return RedirectToAction("Calendar", "Home");
                //return RedirectToAction("CalendarUpdate", new {@updatedEventData = model });
            }

            ModelState.AddModelError("", "Error in creating event");
            return View("ErrorEventCreate");// sweetalert  
        }

        /// <summary>
        /// Return owner calendar with updated event 
        /// </summary>
        /// <param name="model">updated event</param>
        /// <returns></returns>
        public async Task<IActionResult> Update(EventDto eventDto)
        {
            bool updated = await _eventService.Update(eventDto);
            if (updated)
                return RedirectToAction("Calendar", "Home");

            ModelState.AddModelError("", "Error during event update");
            return View(); //view with errors
            //var script = string.Format("var $calendar = $('#calendarCtrl').fullCalendar('getCalendar'); $calendar.fullCalendar('updateEvent', {0})", JsonConvert.SerializeObject(updatedEventData));
            //return new JavaScriptResult(script); //execute update event script
        }

        /// <summary>
        /// Delete event 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(Guid id)
        {
            bool deleted = await _eventService.Delete(id);
            if(deleted)
                return Ok("Event deleted\\canceled !");

            return BadRequest("Event cannot be deleted"); //TODO:
        }

        /// <summary>
        /// Register participant on the event
        /// </summary>
        /// <param name="eventModel"></param>
        /// <param name="participants"></param>
        /// <returns></returns>
        public JavaScriptResult RegisterParticipant(EventDto eventModel, params ApplicationUser[] participants)
        {
            foreach(var participant in participants)
            {
                var who = _mapper.Map<ApplicationUserDto >(participant);
                _eventService.RegisterUser(eventModel, who);
            }
            var script = string.Format("$calendar.fullCalendar('updateEvent', {0})", JsonConvert.SerializeObject(eventModel));
            return new JavaScriptResult(script);
        }

        #region Views

        public IActionResult Index()
        {
            return View();
        }
        // GET: /<controller>/
        public IActionResult AllEvents()
        {
            return View("AllEventsView");
        }

        public IActionResult CultureEvents()
        {
            return View();
        }

        public IActionResult TechEvents()
        {
            return View();
        }

        #endregion
    }
}
