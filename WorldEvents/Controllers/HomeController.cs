using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WorldEvents.Events.Dto;
using WorldEvents.Models;

namespace WorldEvents.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Calendar()
        {
            ViewData["Message"] = "Scheduled events";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "World innovation events ";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
