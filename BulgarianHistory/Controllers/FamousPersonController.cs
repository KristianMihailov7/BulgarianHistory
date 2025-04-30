using BulgarianHistory.Data;
using BulgarianHistory.Data.Entities;
using BulgarianHistory.Web.ViewModel.FamousPerson;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BulgarianHistory.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FamousPersonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FamousPersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FamousPerson
        public async Task<IActionResult> Index()
        {
            var people = _context.FamousPeople.Include(fp => fp.Era);
            return View(await people.ToListAsync());
        }

        // GET: FamousPerson/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var person = await _context.FamousPeople
                .Include(fp => fp.Era)
                .Include(fp => fp.EventFamousPeople).ThenInclude(efp => efp.Event)
                .FirstOrDefaultAsync(fp => fp.Id == id);

            if (person == null) return NotFound();

            var viewModel = new FamousPersonDetailsViewModel
            {
                Id = person.Id,
                Name = person.Name,
                Description = person.Description,
                ImageUrl = person.ImageUrl,
                EraName = person.Era?.Name,
                Events = person.EventFamousPeople.Select(efp => new SimpleItemWithYearViewModel
                {
                    Id = efp.Event.Id,
                    Name = efp.Event.Name,
                    Year = efp.Event.Year
                }).OrderBy(e => e.Year).ToList()
            };

            return View(viewModel);
        }

        // GET: FamousPerson/Create
        public IActionResult Create()
        {
            ViewData["EraId"] = new SelectList(_context.Eras, "Id", "Name");
            return View();
        }

        // POST: FamousPerson/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ImageUrl,EraId")] FamousPerson famousPerson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(famousPerson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EraId"] = new SelectList(_context.Eras, "Id", "Name", famousPerson.EraId);
            return View(famousPerson);
        }

        // GET: FamousPerson/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var famousPerson = await _context.FamousPeople.FindAsync(id);
            if (famousPerson == null) return NotFound();

            ViewData["EraId"] = new SelectList(_context.Eras, "Id", "Name", famousPerson.EraId);
            return View(famousPerson);
        }

        // POST: FamousPerson/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImageUrl,EraId")] FamousPerson famousPerson)
        {
            if (id != famousPerson.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(famousPerson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamousPersonExists(famousPerson.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EraId"] = new SelectList(_context.Eras, "Id", "Name", famousPerson.EraId);
            return View(famousPerson);
        }

        // GET: FamousPerson/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var famousPerson = await _context.FamousPeople
                .Include(fp => fp.Era)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (famousPerson == null) return NotFound();

            return View(famousPerson);
        }

        // POST: FamousPerson/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var famousPerson = await _context.FamousPeople.FindAsync(id);
            if (famousPerson != null)
            {
                _context.FamousPeople.Remove(famousPerson);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool FamousPersonExists(int id)
        {
            return _context.FamousPeople.Any(e => e.Id == id);
        }
    }
}