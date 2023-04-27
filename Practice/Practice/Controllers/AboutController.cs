using Microsoft.AspNetCore.Mvc;
using Practice.Data;
using Practice.Services.Interfaces;

namespace Practice.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        public AboutController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
