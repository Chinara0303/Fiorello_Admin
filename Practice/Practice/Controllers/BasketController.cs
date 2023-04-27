using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Practice.Data;
using Practice.Models;
using Practice.Services;
using Practice.Services.Interfaces;
using Practice.ViewModels.Basket;

namespace Practice.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBasketService _basketService;
        private readonly ILayoutService _layoutService;
        public BasketController(AppDbContext context,IBasketService basketService, ILayoutService layoutService)
        {
            _context = context;
            _basketService = basketService;
            _layoutService = layoutService;
        }
        public async  Task<IActionResult> Index()
        {
            List<BasketVM> baskets = _basketService.GetDatasFromCookie();
            List<BasketDetailVM> basketDetailVMs = new(); 

            foreach (var item in baskets)
            {
                Product dbProduct = _context.Products
                    .Include(p=>p.Images)
                    .FirstOrDefault(p => p.Id == item.Id);
                basketDetailVMs.Add(new BasketDetailVM()
                {
                    Id = dbProduct.Id,
                    Name =  dbProduct.Name,
                    Price = dbProduct.Price,
                    Image = dbProduct.Images.FirstOrDefault(i=>i.IsMain).Image,
                    Count = item.Count,
                    Total = dbProduct.Price * item.Count
                });
            }
            return View(basketDetailVMs);
        }
     

        public IActionResult DeleteDataFromBasket(int? id)
        {
            if (id is null) return BadRequest();

            _layoutService.DeleteData((int)id);
            List<BasketVM> baskets = _basketService.GetDatasFromCookie();

            return Ok();

        }
        public IActionResult IncrementProductCount(int? id)
        {
            if (id is null) return BadRequest();
            var baskets = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            var count =  baskets.FirstOrDefault(b => b.Id == id).Count++;
            
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(baskets));

            return Ok(count);
        }
        public IActionResult DecrementProductCount(int? id)
        {
            if (id is null) return BadRequest();
            var baskets = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            var product = (baskets.FirstOrDefault(b => b.Id == id));
            if(product.Count == 1)
            {
                return Ok();
            }
            var count = product.Count--;
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(baskets));

            return Ok(count);
        }
    }
}
