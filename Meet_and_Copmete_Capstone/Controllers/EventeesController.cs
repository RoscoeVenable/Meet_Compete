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

namespace Meet_and_Copmete_Capstone.Controllers
{
    [Authorize(Roles = "Eventee")]
    public class EventeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        Geocoding geocoding = new Geocoding();

        public EventeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Eventees
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var eventeeLoggedIN = _context.Eventee.Where(e => e.IdentityUserId == userId).SingleOrDefault();

            var eventteeEvents = _context.Event.Where(e => e.ZipCode == eventeeLoggedIN.ZipCode).ToList();

            return View(eventteeEvents);
        }

        // GET: Eventees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventee = await _context.Eventee
                .Include(e => e.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventee == null)
            {
                return NotFound();
            }

            return View(eventee);
        }

        // GET: Eventees/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Eventees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Eventee eventees)
        {

            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                eventees.IdentityUserId = userId;

                await geocoding.GetGeoCodingEventee(eventees);
                _context.Add(eventees);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index"); ;
            }
            catch
            {
                return View();
            }
            
        }

        // GET: Eventees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventee = await _context.Eventee.FindAsync(id);
            if (eventee == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", eventee.IdentityUserId);
            return View(eventee);
        }

        // POST: Eventees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Eventee eventee)
        {
            if (id != eventee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventeeExists(eventee.Id))
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", eventee.IdentityUserId);
            return View(eventee);
        }

        // GET: Eventees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventee = await _context.Eventee
                .Include(e => e.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventee == null)
            {
                return NotFound();
            }

            return View(eventee);
        }

        // POST: Eventees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventee = await _context.Eventee.FindAsync(id);
            _context.Eventee.Remove(eventee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventeeExists(int id)
        {
            return _context.Eventee.Any(e => e.Id == id);
        }
        public IActionResult DnDEvents()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var eventeeLoggedIN = _context.Eventee.Where(e => e.IdentityUserId == userId).SingleOrDefault();

            var DnDEvents = _context.Event.Where(e => e.EventType == "DnD").ToList();

            return View(DnDEvents);
        }
        public IActionResult WarhammerEvents()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var eventeeLoggedIN = _context.Eventee.Where(e => e.IdentityUserId == userId).SingleOrDefault();

            var DnDEvents = _context.Event.Where(e => e.EventType == "Warhammer").ToList();

            return View(DnDEvents);
        }
        public IActionResult BasketBallEvents()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var eventeeLoggedIN = _context.Eventee.Where(e => e.IdentityUserId == userId).SingleOrDefault();

            var DnDEvents = _context.Event.Where(e => e.EventType == "Basketball").ToList();

            return View(DnDEvents);
        }
        public ActionResult AcceptInvite(int target)
        {
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> EventPlanerProfile(int id)
        {
            var eventplaner = await _context.EventPlaner
                .Include(e => e.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventplaner == null)
            {
                return NotFound();
            }
            return View(eventplaner);
        }
    }
}