using BulgarianHistory.Data;
using BulgarianHistory.Web.ViewModel.Timeline;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulgarianHistory.Controllers
{
    public class TimelineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimelineController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var eras = await _context.Eras.Include(e => e.Events).ToListAsync();
            var events = await _context.Events.ToListAsync();

            var model = new TimelineViewModel
            {
                Eras = eras,
                Events = events
            };

            return View(model);
        }
    }
}
