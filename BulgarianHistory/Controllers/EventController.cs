using BulgarianHistory.Data;
using BulgarianHistory.Data.Entities;
using BulgarianHistory.Web.ViewModel.Common;
using BulgarianHistory.Web.ViewModel.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BulgarianHistory.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var events = _context.Events.Include(e => e.Era);
            return View(await events.ToListAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var eventEntity = await _context.Events
                .Include(e => e.Era)
                .Include(e => e.EventCities).ThenInclude(ec => ec.City)
                .Include(e => e.EventFamousPeople).ThenInclude(efp => efp.FamousPerson)
                .Include(e => e.Facts)
                .Include(e => e.Sources)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (eventEntity == null) return NotFound();

            var viewModel = new EventDetailsViewModel
            {
                Id = eventEntity.Id,
                Name = eventEntity.Name,
                Description = eventEntity.Description,
                ImageUrl = eventEntity.ImageUrl,
                Year = eventEntity.Year,
                EraName = eventEntity.Era.Name,
                Cities = eventEntity.EventCities.Select(ec => new SimpleItemViewModel
                {
                    Id = ec.City.Id,
                    Name = ec.City.Name
                }).ToList(),
                FamousPeople = eventEntity.EventFamousPeople.Select(efp => new FamousPersonInEventViewModel
                {
                    Id = efp.FamousPerson.Id,
                    Name = efp.FamousPerson.Name,
                    ImageUrl = efp.FamousPerson.ImageUrl,
                    ShortDescription = efp.FamousPerson.Description?.Length > 100
                        ? efp.FamousPerson.Description.Substring(0, 100) + "..."
                        : efp.FamousPerson.Description
                }).ToList(),
                Facts = eventEntity.Facts.Select(f => $"{f.Title}: {f.Content}").ToList(),
                Sources = eventEntity.Sources.Select(s => new SourceViewModel
                {
                    Title = s.Title,
                    Url = s.Url
                }).ToList(),
                RelatedEvents = await _context.Events
                    .Where(e => e.EraId == eventEntity.EraId && e.Id != id)
                    .Select(e => new SimpleItemViewModel
                    {
                        Id = e.Id,
                        Name = e.Name
                    }).ToListAsync()
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            ViewData["EraId"] = new SelectList(_context.Eras, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ImageUrl,Year,EraId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EraId"] = new SelectList(_context.Eras, "Id", "Name", @event.EraId);
            return View(@event);
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var @event = await _context.Events.FindAsync(id);
            if (@event == null) return NotFound();

            ViewData["EraId"] = new SelectList(_context.Eras, "Id", "Name", @event.EraId);
            return View(@event);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImageUrl,Year,EraId")] Event @event)
        {
            if (id != @event.Id)
                return NotFound();

            // 🧠 Important: If EraId == 0, the user didn't select a value
            if (@event.EraId == 0)
                ModelState.AddModelError("EraId", "Моля, изберете епоха.");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
                        return NotFound();
                    else
                        throw;
                }
            }

            // ✅ This repopulates the dropdown with the correct selected value
            ViewData["EraId"] = new SelectList(_context.Eras, "Id", "Name", @event.EraId);
            return View(@event);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var @event = await _context.Events
                .Include(e => e.Era)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (@event == null) return NotFound();

            return View(@event);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
