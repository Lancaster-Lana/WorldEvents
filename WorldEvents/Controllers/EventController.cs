using Microsoft.AspNetCore.Mvc;

namespace WorldEvents.Controllers
{
    public class EventController : Controller
    {
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
    }
}
