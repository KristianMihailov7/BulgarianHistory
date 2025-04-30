using BulgarianHistory.Data;
using BulgarianHistory.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BulgarianHistory.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FactController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fact
        public async Task<IActionResult> Index()
        {
            var facts = _context.Facts.Include(f => f.Event);
            return View(await facts.ToListAsync());
        }

        // GET: Fact/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var fact = await _context.Facts
                .Include(f => f.Event)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (fact == null) return NotFound();

            return View(fact);
        }

        // GET: Fact/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Name");
            return View();
        }

        // POST: Fact/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,ImageUrl,EventId")] Fact fact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Name", fact.EventId);
            return View(fact);
        }

        // GET: Fact/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var fact = await _context.Facts.FindAsync(id);
            if (fact == null) return NotFound();

            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Name", fact.EventId);
            return View(fact);
        }

        // POST: Fact/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,ImageUrl,EventId")] Fact fact)
        {
            if (id != fact.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FactExists(fact.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Name", fact.EventId);
            return View(fact);
        }

        // GET: Fact/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var fact = await _context.Facts
                .Include(f => f.Event)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (fact == null) return NotFound();

            return View(fact);
        }

        // POST: Fact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fact = await _context.Facts.FindAsync(id);
            if (fact != null)
            {
                _context.Facts.Remove(fact);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool FactExists(int id)
        {
            return _context.Facts.Any(e => e.Id == id);
        }
    }
}