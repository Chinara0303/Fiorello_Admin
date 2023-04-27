using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Practice.Areas.Admin.ViewModels;
using Practice.Data;
using Practice.Helpers;
using Practice.Models;
using Practice.Services.Interfaces;

namespace Practice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;

        public ProductController(IProductService productService,
                                 ICategoryService categoryService,
                                 IWebHostEnvironment env,
                                 AppDbContext context)
        {
            _productService = productService;
            _categoryService = categoryService;
            _env = env;
            _context = context; 
        }
        public async Task<IActionResult> Index(int page = 1, int take = 5)
        {
            List<Product> datas = await _productService.GetPaginatedDatasAsync(page, take);
            List<ProductListVM> mappedDatas = GetDatas(datas);

            int pageCount = await GetPageCountAsync(take);

            Paginate<ProductListVM> paginatedDatas = new(mappedDatas, page, pageCount);

            return View(paginatedDatas);
        }
        private async Task<int> GetPageCountAsync(int take)
        {
            var productCount = await _productService.GetCountAsync();
            return (int)Math.Ceiling((decimal)productCount / take);
        }
        private List<ProductListVM> GetDatas(List<Product> products)
        {
            List<ProductListVM> mappedDatas = new();
            foreach (var product in products)
            {
                ProductListVM productList = new()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    CategoryName = product.Category.Name,
                    Image = product.Images.Where(p => p.IsMain).FirstOrDefault().Image
                };
                mappedDatas.Add(productList);
            }
            return mappedDatas;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.categories = await GetCategories();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductVM model)
        {
            try
            {
                ViewBag.categories = await GetCategories();

                if (!ModelState.IsValid) return View(model);

                foreach (var photo in model.Photos)
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
                List<ProductImage> productImages = new();

                foreach (var photo in model.Photos)
                {
                    ProductImage productImage = new()
                    {
                        Image = photo.CreateFile(_env, "img")
                    };
                    productImages.Add(productImage);
                }

                productImages.FirstOrDefault().IsMain = true;

                var convertedPrice = decimal.Parse(model.Price);

                Product newProduct = new()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = convertedPrice,
                    Images = productImages,
                    CategoryId = model.CategoryId
                };

                await _context.ProductImages.AddRangeAsync(productImages);
                await _context.Products.AddAsync(newProduct);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }
        private async Task<SelectList> GetCategories()
        {
            List<Category> categories = await _categoryService.GetAll();
            return new SelectList(categories, "Id", "Name");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            Product product = await _productService.GetFullDataByIdAsync((int)id);
            if (product is null) return NotFound();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id is null) return BadRequest();
            Product product = await _productService.GetFullDataByIdAsync((int)id);
            if (product is null) return NotFound();

            foreach (var item in product.Images)
            {
                string path = FileHelper.GetFilePath(_env.WebRootPath, "img", item.Image);
                FileHelper.DeleteFile(path);
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
