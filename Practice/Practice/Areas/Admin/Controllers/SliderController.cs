using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice.Areas.Admin.ViewModels;
using Practice.Data;
using Practice.Helpers;
using Practice.Models;

namespace Practice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context,
                                IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Slider> sliders = await _context.Sliders.ToListAsync();
            return View(sliders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Slider slider)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return View();
        //        }
        //        if (!slider.Photo.CheckFileType("image/"))
        //        {
        //            ModelState.AddModelError("Photo", "File type must be image");
        //            return View();
        //        }
        //        if (!slider.Photo.CheckFileSize(200))
        //        {
        //            ModelState.AddModelError("Photo", "Image size must be max 200kb");
        //            return View();
        //        }

        //        slider.Image = slider.Photo.CreateFile(_env, "img");

        //        await _context.AddAsync(slider);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));

        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.error = ex.Message;
        //        return View();
        //    }

        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM slider)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                foreach (var photo in slider.Photos)
                {
                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View();
                    }
                    if (!photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View();
                    }
                }
                foreach (var photo in slider.Photos)
                {
                    Slider newSlider = new()
                    {
                        Image = photo.CreateFile(_env, "img")
                    };
                    await _context.Sliders.AddAsync(newSlider);
                }
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
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider is null) return NotFound();

            return View(slider);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider is null) return NotFound();

            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Slider slider)
        {
            try
            {
                if (id == null) return BadRequest();
                Slider dbSlider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
                if (dbSlider is null) return NotFound();

                if (slider.Photo == null) return RedirectToAction(nameof(Index));

                if (!slider.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(dbSlider);
                }
                if (!slider.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 200kb");
                    return View(dbSlider);
                }

                string oldPath = FileHelper.GetFilePath(_env.WebRootPath, "img", dbSlider.Image);
                FileHelper.DeleteFile(oldPath);

                dbSlider.Image = slider.Photo.CreateFile(_env, "img");

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
            try
            {
                if (id == null) return BadRequest();
                Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
                if (slider is null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "img", slider.Image);
                FileHelper.DeleteFile(path);

                _context.Sliders.Remove(slider);
                await _context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }

        }

        [HttpPost]
        public async Task<IActionResult> SetStatus(int? id)
        {
            if (id == null) return BadRequest();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider is null) return NotFound();

            slider.SoftDelete = !slider.SoftDelete;

            await _context.SaveChangesAsync();

            return Ok(slider.SoftDelete);
        }
    }
}
