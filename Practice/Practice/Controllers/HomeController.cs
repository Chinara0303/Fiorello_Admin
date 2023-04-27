using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Practice.Data;
using Practice.Models;
using Practice.Services;
using Practice.Services.Interfaces;
using Practice.ViewModels.Basket;
using Practice.ViewModels.Home;

namespace Practice.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;
        public HomeController(AppDbContext context,
                              IBasketService basketService,
                              IProductService productService)
        {
            _context = context;
            _basketService = basketService;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {

            IEnumerable<Product> products = await _context.Products
                .Include(p=>p.Images)
                .ToListAsync();
            IEnumerable<Category> categories = await _context.Categories
                .ToListAsync();
            About about = await _context.Abouts
                .FirstOrDefaultAsync();
            Subscribe subscribe = await _context.Subscribes
                .FirstOrDefaultAsync();
            IEnumerable<Author> authors = await _context.Authors
                .Include(a=>a.Says)
                .ToListAsync();
            IEnumerable<Instagram> instagrams = await _context.Instagrams
                .ToListAsync();


            HomeVM model = new()
            {
                Products = products,
                Categories = categories,
                About = about,
                Subscribe = subscribe,
                Authors = authors,
                Instagrams = instagrams
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null) return BadRequest();

            Product dbProduct = await _productService.GetByIdAsync((int)id);

            if (dbProduct == null) return NotFound();

            List<BasketVM> baskets = _basketService.GetDatasFromCookie();

            BasketVM existProduct = baskets.FirstOrDefault(p => p.Id == id);

            _basketService.SetDatasToCookie(baskets, existProduct, dbProduct);

            var basketCount = baskets.Count();

            return Ok(basketCount);
        }

    }
}