using BulgarianHistory.Data;
using BulgarianHistory.Models;
using BulgarianHistory.Web.ViewModel.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulgarianHistory.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var eras = _context.Eras
                .Select(e => new EraViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    ImageUrl = e.ImageUrl
                }).ToList();

            return View(eras);
        }
    }
}
