using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Models;

namespace Practice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExpertHeaderController : Controller
    {
        private readonly AppDbContext _context;
        public ExpertHeaderController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index() => View(await _context.ExpertHeaders.ToListAsync());

        [HttpGet]
        public  IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpertHeader expertHeader)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                await _context.ExpertHeaders.AddAsync(expertHeader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var dbExpertHeader = await _context.ExpertHeaders.FirstOrDefaultAsync(eh=>eh.Id == id);
            if (dbExpertHeader is null) return NotFound();

            return View(dbExpertHeader);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ExpertHeader expertHeader)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (id is null) return BadRequest();
                var dbExpertHeader = await _context.ExpertHeaders.AsNoTracking().FirstOrDefaultAsync(eh => eh.Id == id);
                if (dbExpertHeader is null) return NotFound();

                _context.ExpertHeaders.Update(expertHeader);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var dbExpertHeader = await _context.ExpertHeaders.AsNoTracking().FirstOrDefaultAsync(eh => eh.Id == id);
            if (dbExpertHeader is null) return NotFound();

             _context.ExpertHeaders.Remove(dbExpertHeader);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
