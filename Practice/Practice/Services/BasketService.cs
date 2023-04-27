using Newtonsoft.Json;
using Practice.Models;
using Practice.Services.Interfaces;
using Practice.ViewModels.Basket;
using System.Linq;

namespace Practice.Services
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<BasketVM> GetDatasFromCookie()
        {
            List<BasketVM> baskets;

            if (_httpContextAccessor.HttpContext.Request.Cookies["basket"] != null)
            {
                baskets = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                baskets = new List<BasketVM>();
            }
            return baskets;
        }

        public void SetDatasToCookie(List<BasketVM> baskets, BasketVM existProduct, Product dbProduct)
        {
            if (existProduct == null)
            {
                baskets.Add(new BasketVM
                {
                    Id = dbProduct.Id,
                    Count = 1
                });
            }
            else
            {
                existProduct.Count++;
            }
            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(baskets));
        }
    }
}
