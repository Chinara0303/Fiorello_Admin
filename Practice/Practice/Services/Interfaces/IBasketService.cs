using Practice.Models;
using Practice.ViewModels.Basket;

namespace Practice.Services.Interfaces
{
    public interface IBasketService
    {
        List<BasketVM> GetDatasFromCookie();
        void SetDatasToCookie(List<BasketVM> baskets, BasketVM existProduct, Product dbProduct);

    }
}
