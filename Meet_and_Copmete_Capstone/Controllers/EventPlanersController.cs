using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Meet_and_Copmete_Capstone.Data;
using Meet_and_Copmete_Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Meet_and_Copmete_Capstone.ActionFilters;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;

namespace Meet_and_Copmete_Capstone.Controllers
{
    [Authorize(Roles = "EventPlaner")]
    public class EventPlanersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventPlanersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EventPlaners
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var eventPlannerLoggedIn = _context.EventPlaner.Where(e => e.IdentityUserId == userId).FirstOrDefault();

            var eventPlannerEvents = _context.Event.Where(e => e.EventPlannerId == eventPlannerLoggedIn.Id).ToList();
            //GetGeocoding();

            return View(eventPlannerEvents);
        }

        // GET: EventPlaners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventPlaner = await _context.EventPlaner
                .Include(e => e.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventPlaner == null)
            {
                return NotFound();
            }

            return View(eventPlaner);
        }

        public async Task<IActionResult> UploadPage()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var eventPlaner = _context.EventPlaner.Where(e => e.IdentityUserId == userId).SingleOrDefault();

            if (eventPlaner == null)
            {
                return NotFound();
            }

            return View(eventPlaner);
        }

        // GET: EventPlaners/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        private async Task<bool> GetNearbyPeople(int eventId)
        {
            string eventAddr = "";
            string[] eventCoords = GetCoordinates(eventAddr);

            List<string[]> eventeeCoordinates = await _context.Eventee.Select(e => GetCoordinates(e.Address)).ToListAsync();
            List<string> distance = new List<string>();
            foreach (var pair in eventeeCoordinates)
            {
                distance.Add(CalculateDistance(pair[0], pair[1], eventCoords[0], eventCoords[1]));
            }

            
            return true;
        }

        private string CalculateDistance(string latSource, string lngSource, string latDest, string lngDest)
        {
            return "";
        }

        private string[] GetCoordinates(string address)
        {
            return new string[2];
        }
        private async Task<bool> GetGeocoding()
        {
            string fullAddress = Uri.EscapeDataString("123 nw 7th st,Oak Island,NC");
            string geocodingRequest = $"https://maps.googleapis.com/maps/api/geocode/json?address={fullAddress}&key={Secrets.APIKEY}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(geocodingRequest);
            string jsonResult = await response.Content.ReadAsStringAsync();
            string lng1 ="";
            string lat1 ="";
            if (jsonResult != null)
            {
                JObject jobject = JObject.Parse(jsonResult);
                lng1 = (string)jobject["results"][0]["geometry"]["location"]["lng"];
                lat1 = (string)jobject["results"][0]["geometry"]["location"]["lat"];
            }

            fullAddress = Uri.EscapeDataString("123 nw 8th st,Oak Island,NC");
            geocodingRequest = $"https://maps.googleapis.com/maps/api/geocode/json?address={fullAddress}&key={Secrets.APIKEY}";
            client = new HttpClient();
            response = await client.GetAsync(geocodingRequest);
            jsonResult = await response.Content.ReadAsStringAsync();
            string lng2 = "";
            string lat2 = "";
            if (jsonResult != null)
            {
                JObject jobject = JObject.Parse(jsonResult);
                lng2 = (string)jobject["results"][0]["geometry"]["location"]["lng"];
                lat2 = (string)jobject["results"][0]["geometry"]["location"]["lat"];
            }

            //List<string[]> eventCoordinates = new List<string[]>();
            //string userLng;
            //string userLat;

            //string firstPart = "https://maps.googleapis.com/maps/api/distancematrix/json?";
            //string apiPart = $"&key={Secrets.APIKEY}";
            //string origins = "origins=";
            //string destinations = "&destinations=";
            //for (int i = 0; i < eventCoordinates.Count; i++)
            //{
            //    if (i != 0) {
            //        origins += $"|{eventCoordinates[i][0]}, {eventCoordinates[i][1]}";
            //        destinations += $"|{eventCoordinates[i][0]}, {eventCoordinates[i][1]}";
            //    } else
            //    {
            //        origins += $"{eventCoordinates[i][0]}, {eventCoordinates[i][1]}";
            //        destinations += $"{eventCoordinates[i][0]}, {eventCoordinates[i][1]}";
            //    }
            //}
            

            string distanceRequest = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={lat1},{lng1}&destinations={lat2},{lng2}&key={Secrets.APIKEY}";
            client = new HttpClient();
            response = await client.GetAsync(distanceRequest);
            jsonResult = await response.Content.ReadAsStringAsync();
            if (jsonResult != null)
            {
                JObject jobject = JObject.Parse(jsonResult);
                var rows = jobject["rows"];
                foreach (var item in rows)
                {
                    
                    foreach (var value in item["elements"])
                    {
                        string temp = value["distance"]["value"].ToString();
                        var temp2 = value["distance"]["value"];
                        
                    }
                }
            }

            return true;

        }

        // POST: EventPlaners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LoginEmail,FirstName,LastName,ZipCode")] EventPlaner eventPlaner)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                eventPlaner.IdentityUserId = userId;

                _context.EventPlaner.Add(eventPlaner);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", eventPlaner.IdentityUserId);
            return View(eventPlaner);
        }

        // GET: EventPlaners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var eventPlannerLoggedIn = _context.EventPlaner.Where(e => e.IdentityUserId == userId).SingleOrDefault();

            if (eventPlannerLoggedIn == null)
            {
                return NotFound();
            }

            //var eventPlaner = await _context.EventPlaner.FindAsync(id);
            //if (eventPlaner == null)
            //{
            //    return NotFound();
            //}
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", eventPlannerLoggedIn.IdentityUserId);
            return View(eventPlannerLoggedIn);
        }

        // POST: EventPlaners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventPlaner eventPlaner)
        {
            if (id != eventPlaner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventPlaner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventPlanerExists(eventPlaner.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", eventPlaner.IdentityUserId);
            return View(eventPlaner);
        }

        // GET: EventPlaners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventPlaner = await _context.EventPlaner
                .Include(e => e.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventPlaner == null)
            {
                return NotFound();
            }

            return View(eventPlaner);
        }

        // POST: EventPlaners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventPlaner = await _context.EventPlaner.FindAsync(id);
            _context.EventPlaner.Remove(eventPlaner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventPlanerExists(int id)
        {
            return _context.EventPlaner.Any(e => e.Id == id);
        }

        [HttpPut]
        public ActionResult Upload([FromForm] IFormFile file)
        {
            return Ok();
        }
    }
}
