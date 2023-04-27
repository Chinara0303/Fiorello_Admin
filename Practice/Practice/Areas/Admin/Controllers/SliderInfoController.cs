using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice.Areas.Admin.ViewModels;
using Practice.Data;
using Practice.Helpers;
using Practice.Models;

namespace Practice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderInfoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderInfoController(AppDbContext context,
                                    IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<SliderInfo> sliderInfos = await _context.SliderInfos.ToListAsync();
            return View(sliderInfos);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var dbSliderInfo = await _context.SliderInfos.FirstOrDefaultAsync(si => si.Id == id);
            if (dbSliderInfo is null) return NotFound();
            return View(dbSliderInfo);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderInfo sliderInfo)
        {
            try
            {
                if (!ModelState.IsValid) return View();
                if (!sliderInfo.SignaturePhoto.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View();
                }
                if (!sliderInfo.SignaturePhoto.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 200kb");
                    return View();
                }

                sliderInfo.SignatureImage = sliderInfo.SignaturePhoto.CreateFile(_env, "img");

                await _context.AddAsync(sliderInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            SliderInfo dbSliderInfo = await _context.SliderInfos.FirstOrDefaultAsync(si => si.Id == id);
            if (dbSliderInfo is null) return NotFound();

            string path = FileHelper.GetFilePath(_env.WebRootPath, "img", dbSliderInfo.SignatureImage);
            FileHelper.DeleteFile(path);

             _context.Remove(dbSliderInfo);
            await _context.SaveChangesAsync();
            
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            SliderInfo dbSliderInfo = await _context.SliderInfos.FirstOrDefaultAsync(si => si.Id == id);
            if (dbSliderInfo is null) return NotFound();
            SliderInfoUpdateVM model = new()
            {
                SignatureImage = dbSliderInfo.SignatureImage,
                Title = dbSliderInfo.Title,
                Description = dbSliderInfo.Description
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,SliderInfoUpdateVM sliderInfo)
        {
            try
            {
                if (id is null) return BadRequest();
                SliderInfo dbSliderInfo = await _context.SliderInfos.FirstOrDefaultAsync(si => si.Id == id);
                if (dbSliderInfo is null) return NotFound();
                SliderInfoUpdateVM model = new()
                {
                    SignatureImage = dbSliderInfo.SignatureImage,
                    Title = dbSliderInfo.Title,
                    Description = dbSliderInfo.Description
                };
                if (!ModelState.IsValid) return View(model);

                if (sliderInfo.SignaturePhoto is not null)
                {
                    if (!sliderInfo.SignaturePhoto.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!sliderInfo.SignaturePhoto.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View(model);
                    }
                    string oldPath = FileHelper.GetFilePath(_env.WebRootPath, "img", dbSliderInfo.SignatureImage);
                    FileHelper.DeleteFile(oldPath);

                    dbSliderInfo.SignatureImage = sliderInfo.SignaturePhoto.CreateFile(_env, "img");
                }
                else
                {
                    SliderInfo slider = new()
                    {
                        SignatureImage = dbSliderInfo.SignatureImage
                    };
                }

                dbSliderInfo.Title = sliderInfo.Title;
                dbSliderInfo.Description = sliderInfo.Description;

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
