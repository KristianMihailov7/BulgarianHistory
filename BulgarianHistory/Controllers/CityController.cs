using BulgarianHistory.Data;
using BulgarianHistory.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BulgarianHistory.Web.ViewModel.City;
using BulgarianHistory.Web.ViewModel.Common;

namespace BulgarianHistory.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CityController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: City
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cities.ToListAsync());
        }

        // GET: City/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var city = await _context.Cities
                .Include(c => c.EventCities).ThenInclude(ec => ec.Event).ThenInclude(e => e.EventFamousPeople).ThenInclude(efp => efp.FamousPerson)
                .Include(c => c.EventCities).ThenInclude(ec => ec.Event).ThenInclude(e => e.Era)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (city == null) return NotFound();

            var relatedEvents = city.EventCities
                .Select(ec => ec.Event)
                .Distinct()
                .ToList();

            var famousPeople = relatedEvents
                .SelectMany(e => e.EventFamousPeople)
                .Select(fp => fp.FamousPerson)
                .Distinct()
                .ToList();

            var viewModel = new CityDetailsViewModel
            {
                Id = city.Id,
                Name = city.Name,
                Description = city.Description,
                ImageUrl = city.ImageUrl,
                RelatedEvents = relatedEvents
                    .Select(e => new SimpleItemViewModel { Id = e.Id, Name = $"{e.Name} ({e.Era.Name})" })
                    .ToList(),
                FamousPeople = famousPeople
                    .Select(fp => new SimpleItemViewModel { Id = fp.Id, Name = fp.Name })
                    .ToList()
            };

            return View(viewModel);
        }

        // GET: City/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: City/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ImageUrl,Latitude,Longitude")] City city)
        {
            if (ModelState.IsValid)
            {
                _context.Add(city);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        // GET: City/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var city = await _context.Cities.FindAsync(id);
            if (city == null) return NotFound();

            return View(city);
        }

        // POST: City/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImageUrl,Latitude,Longitude")] City city)
        {
            if (id != city.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        // GET: City/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var city = await _context.Cities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (city == null) return NotFound();

            return View(city);
        }

        // POST: City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city != null)
            {
                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.Id == id);
        }
    }
}