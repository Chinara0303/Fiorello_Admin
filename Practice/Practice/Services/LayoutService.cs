using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Practice.Data;
using Practice.Models;
using Practice.Services.Interfaces;
using Practice.ViewModels.Basket;
using Practice.ViewModels.Layout;

namespace Practice.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBasketService _basketService;
        private readonly AppDbContext _context;

        public LayoutService(IHttpContextAccessor httpContextAccessor,
                             IBasketService basketService,
                             AppDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _basketService = basketService;
            _context = context;
        }

        public void DeleteData(int? id)
        {
            var baskets = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            var deletedProduct = baskets.FirstOrDefault(b => b.Id == id);
            baskets.Remove(deletedProduct);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(baskets));
        }

        public LayoutVM GetSettingDatas()
        {
            Dictionary<string, string> settings = _context.Settings
                .AsEnumerable()
                .ToDictionary(s => s.Key, s => s.Value);
          
            return new LayoutVM {
                Settings = settings,
                BasketCount = _basketService.GetDatasFromCookie().Count(),
            };
        }
    }
}
