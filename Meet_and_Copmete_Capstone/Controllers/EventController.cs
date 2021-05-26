using Meet_and_Copmete_Capstone.ActionFilters;
using Meet_and_Copmete_Capstone.Data;
using Meet_and_Copmete_Capstone.Models;
using Meet_and_Copmete_Capstone.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Meet_and_Copmete_Capstone.Controllers
{

    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;
        Geocoding geocoding = new Geocoding();

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: EventController
        public ActionResult Index()
        {
            ViewData["APIkeys"] = Secrets.APIKEY;
            return View();
            //if (_context.UserRoles = "EventPlanner")
            //{
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var eventPlannerLoggedIn = _context.EventPlaner.Where(e => e.IdentityUserId == userId).SingleOrDefault();

            var eventPlannerEvents = _context.Event.Where(e => e.EventPlannerId == eventPlannerLoggedIn.Id).ToList();

            return View(eventPlannerEvents);
            //}
            //else
            //{
            //    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //    var eventeeLoggedIN = _context.Eventee.Where(e => e.IdentityUserId == userId).SingleOrDefault();

            //    var eventteeEvents = _context.Event.Where(e => e.ZipCode == eventeeLoggedIN.ZipCode).ToList();

            //    return View(eventteeEvents);
            //}
        }

        // GET: EventController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ViewData["APIkeys"] = Secrets.APIKEY;
            var eventees = _context.Eventee.ToList();
            var events = _context.Event.Find(id);
            ViewBag.people = await geocoding.GetNearbyPeople(eventees, events);
            
            return View(events);
        }
        public IActionResult Detail(int id)
        {
          var events = _context.Event.Find(id);

            return View(events);
        }

        // GET: EventController/Create
        public ActionResult EventCreate()
        {
            ViewData["APIkeys"] = Secrets.APIKEY;
            List<EventTypes> eventTypes = new List<EventTypes>();
            eventTypes.Add(new EventTypes { Value = "DnD", Text = "DnD", Selected = true});
            eventTypes.Add(new EventTypes { Value = "Basketball", Text = "Basketball", Selected = false });
            eventTypes.Add(new EventTypes { Value = "Warhammer", Text = "Warhammer", Selected = false });
            ViewBag.EventTypes = new SelectList(eventTypes, "Value", "Text");
            return View();
        }

        // POST: EventController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EventCreate(Event events)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var eventPlannerLoggedIn = _context.EventPlaner.Where(e => e.IdentityUserId == userId).SingleOrDefault();
                events.EventPlannerId = eventPlannerLoggedIn.Id;

                await geocoding.GetGeoCoding(events);
                await _context.AddAsync(events);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "EventPlaners");
            }
            catch
            {
                return View();
            }
        }

        // GET: EventController/Edit/5
        public IActionResult Edit(int id)
        {
            ViewData["APIkeys"] = Secrets.APIKEY;
            var events = _context.Event.Find(id);
            return View(events);
        }

        // POST: EventController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Event events)
        {
            
            try
            {
                
                _context.Update(events);
                _context.SaveChanges();
                return RedirectToAction("Index", "EventPlaners");
            }
            catch
            {
                return View();
            }
        }

        // GET: EventController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var events = _context.Event.Find(id);
                _context.Event.Remove(events);
                _context.SaveChanges();
                return RedirectToAction("Index", "EventPlaners");
            }
            catch
            {
                return View();
            }
        }

    }
}
