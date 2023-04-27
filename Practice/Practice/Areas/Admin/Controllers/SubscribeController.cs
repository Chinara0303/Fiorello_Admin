using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice.Areas.Admin.ViewModels;
using Practice.Data;
using Practice.Helpers;
using Practice.Models;

namespace Practice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubscribeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SubscribeController(AppDbContext context,
                              IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Subscribe> subscribes = await _context.Subscribes.ToListAsync();
            return View(subscribes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Subscribe subscribe)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                if (!subscribe.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View();
                }
                if (!subscribe.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 200kb");
                    return View();
                }

                subscribe.Image = subscribe.Photo.CreateFile(_env, "img");

                await _context.AddAsync(subscribe);
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
            Subscribe dbSubscribe = await _context.Subscribes.FirstOrDefaultAsync(s => s.Id == id);
            if (dbSubscribe is null) return NotFound();

            string path = FileHelper.GetFilePath(_env.WebRootPath, "img", dbSubscribe.Image);
            FileHelper.DeleteFile(path);

            _context.Remove(dbSubscribe);
            await _context.SaveChangesAsync();

            return Ok();
        }
      
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            Subscribe dbSubscribe = await _context.Subscribes.FirstOrDefaultAsync(s => s.Id == id);
            if (dbSubscribe is null) return NotFound();
            SubscribeUpdateVM model = new()
            {
                Image = dbSubscribe.Image,
                Title = dbSubscribe.Title
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SubscribeUpdateVM subscribe)
        {
            try
            {
                if (id is null) return BadRequest();
                Subscribe dbSubscribe = await _context.Subscribes.FirstOrDefaultAsync(s => s.Id == id);
                if (dbSubscribe is null) return NotFound();
                SubscribeUpdateVM model = new()
                {
                    Image = dbSubscribe.Image,
                    Title = dbSubscribe.Title
                };
                if (!ModelState.IsValid) return View(model);

                if (subscribe.Photo is not null)
                {
                    if (!subscribe.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!subscribe.Photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View(model);
                    }
                    string oldPath = FileHelper.GetFilePath(_env.WebRootPath, "img", dbSubscribe.Image);
                    FileHelper.DeleteFile(oldPath);

                    dbSubscribe.Image = subscribe.Photo.CreateFile(_env, "img");
                }
                else
                {
                    Subscribe newSubscribe = new()
                    {
                        Image = dbSubscribe.Image
                    };
                }

                dbSubscribe.Title = subscribe.Title;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }


        }
    }
}
