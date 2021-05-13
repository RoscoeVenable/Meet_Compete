﻿using Meet_and_Copmete_Capstone.ActionFilters;
using Meet_and_Copmete_Capstone.Data;
using Meet_and_Copmete_Capstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: EventController
        public ActionResult Index()
        {
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
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EventController/Create
        public ActionResult EventCreate()
        {
            return View();
        }

        // POST: EventController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EventCreate(Event events)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var eventPlannerLoggedIn = _context.EventPlaner.Where(e => e.IdentityUserId == userId).SingleOrDefault();

                events.EventPlannerId = eventPlannerLoggedIn.Id;
                _context.Add(events);
                _context.SaveChanges();
                return RedirectToAction("Index", "EventPlaners");
            }
            catch
            {
                return View();
            }
        }

        // GET: EventController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EventController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
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
