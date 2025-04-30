using BulgarianHistory.Data;
using BulgarianHistory.Data.Entities;
using BulgarianHistory.Web.ViewModel.Common;
using BulgarianHistory.Web.ViewModel.Details;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulgarianHistory.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EraController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EraController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Era
        public async Task<IActionResult> Index()
        {
            return View(await _context.Eras.ToListAsync());
        }

        // GET: Era/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var era = await _context.Eras
                .Include(e => e.Events)
                .Include(e => e.Cities)
                .Include(e => e.FamousPeople)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (era == null) return NotFound();

            var viewModel = new EraDetailsViewModel
            {
                Id = era.Id,
                Name = era.Name,
                Description = era.Description,
                ImageUrl = era.ImageUrl,
                Events = era.Events.Select(e => new SimpleItemViewModel { Id = e.Id, Name = e.Name }).ToList(),
                Cities = era.Cities.Select(c => new SimpleItemViewModel { Id = c.Id, Name = c.Name }).ToList(),
                FamousPeople = era.FamousPeople.Select(fp => new SimpleItemViewModel { Id = fp.Id, Name = fp.Name }).ToList()
            };

            return View(viewModel);
        }

        // GET: Era/Create
        public IActionResult Create() => View();

        // POST: Era/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ImageUrl")] Era era)
        {
            if (ModelState.IsValid)
            {
                _context.Add(era);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(era);
        }

        // GET: Era/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var era = await _context.Eras.FindAsync(id);
            if (era == null) return NotFound();

            return View(era);
        }

        // POST: Era/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImageUrl")] Era era)
        {
            if (id != era.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(era);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EraExists(era.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(era);
        }

        // GET: Era/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var era = await _context.Eras.FirstOrDefaultAsync(m => m.Id == id);
            if (era == null) return NotFound();

            return View(era);
        }

        // POST: Era/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var era = await _context.Eras.FindAsync(id);
            if (era != null)
            {
                _context.Eras.Remove(era);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EraExists(int id) => _context.Eras.Any(e => e.Id == id);
    }
}
