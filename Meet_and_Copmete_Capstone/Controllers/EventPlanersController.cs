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

namespace Meet_and_Copmete_Capstone.Controllers
{
    
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
            var eventPlannerLoggedIn = _context.EventPlaner.Where(e => e.IdentityUserId == userId).SingleOrDefault();

            var eventPlannerEvents = _context.Event.Where(e => e.EventPlannerId == eventPlannerLoggedIn.Id).ToList();

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

        // GET: EventPlaners/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
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
                return RedirectToAction("Index", "Event");
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", eventPlaner.IdentityUserId);
            return View(eventPlaner);
        }

        // GET: EventPlaners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventPlaner = await _context.EventPlaner.FindAsync(id);
            if (eventPlaner == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", eventPlaner.IdentityUserId);
            return View(eventPlaner);
        }

        // POST: EventPlaners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LoginEmail,FirstName,LastName,ZipCode,IdentityUserId")] EventPlaner eventPlaner)
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
    }
}
