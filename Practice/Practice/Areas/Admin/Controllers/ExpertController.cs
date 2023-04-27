using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice.Areas.Admin.ViewModels;
using Practice.Data;
using Practice.Helpers;
using Practice.Models;
using Practice.Services.Interfaces;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace Practice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExpertController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IExpertService _expertService;
        private readonly IWebHostEnvironment _env;
        public ExpertController(AppDbContext context,
                                IExpertService expertService,
                                IWebHostEnvironment env)
        {
            _context = context;
            _expertService = expertService;
            _env = env;
        }
        public async Task<IActionResult> Index() => View(await _expertService.GetAll());

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Expert expert)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                if (!expert.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View();
                }
                if (!expert.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "File size must be max 200kb");
                    return View();
                }

                expert.Image = expert.Photo.CreateFile(_env, "img");

                //if (expert.ExpertPositions.Count() > 0)
                //{
                //    List<ExpertExpertPosition> positions = new();
                //    foreach (var item in expert.ExpertPositions)
                //    {
                //        ExpertExpertPosition position = new()
                //        {
                //        };
                //        positions.Add(position);
                //    }
                //   expert.ExpertPositions = positions;
                //}



                await _context.Experts.AddAsync(expert);
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
