using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Practice.Data;
using Practice.Models;
using Practice.Services.Interfaces;
using Practice.ViewModels.Basket;

namespace Practice.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBasketService _basketService;
        public ShopController(AppDbContext context, IBasketService basketService)
        {
            _context = context;
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            int count = await _context.Products
                .Where(p=>!p.SoftDelete)
                .CountAsync();
            ViewBag.Count = count;

            IEnumerable<Product> products = await _context.Products
                .Include(p=>p.Images)
                .Include(p=>p.Category)
                .Where(p=>!p.SoftDelete)
                .Take(4)
                .ToListAsync();

            IEnumerable<Category> categories = await _context.Categories
                .Where(p => !p.SoftDelete)
                .ToListAsync();

            ViewBag.Categories = categories;

            return View(products);
        }
        public async Task<IActionResult> LoadMore(int skip)
        {
            IEnumerable<Product> products = await _context.Products
              .Include(p => p.Images)
              .Where(p => !p.SoftDelete)
              .Skip(skip)
              .Take(4)
              .ToListAsync();

            return PartialView("_ProductsPartial", products);
        }

        [HttpPost]
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null) return BadRequest();

            Product dbProduct = await GetProductById((int)id);

            if (dbProduct == null) return NotFound();

            List<BasketVM> baskets = _basketService.GetDatasFromCookie();

            BasketVM existProduct = baskets.FirstOrDefault(p => p.Id == id);

            var basketCount = baskets.Count();

            _basketService.SetDatasToCookie(baskets, existProduct, dbProduct);

            return Ok(basketCount);
        }
        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }
      
        public async Task<IActionResult> Search(string searchText)
        {
            if (String.IsNullOrWhiteSpace(searchText))
            {
                return Ok();
            }
            var products = await _context.Products
                .Include(p => p.Images)
                .Include(p => p.Category)
                .OrderByDescending(p=>p.Id)
                .Where(p => p.Name.ToLower().Contains(searchText.ToLower()) 
                               || p.Category.Name.ToLower().Contains(searchText.ToLower()))
                .ToListAsync();

            return PartialView("_SearchPartial",products);
        }
    }
}
