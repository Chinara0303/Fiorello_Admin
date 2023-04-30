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
                    Image = product.Images.Where(i => i.IsMain).FirstOrDefault()?.Image
                };
                mappedDatas.Add(productList);
            }
            return mappedDatas;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.categories = await GetCategoriesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductVM model)
        {
            try
            {
                ViewBag.categories = await GetCategoriesAsync();

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
        private async Task<SelectList> GetCategoriesAsync()
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

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            Product product = await _productService.GetFullDataByIdAsync((int)id);
            if (product is null) return NotFound();

            ViewBag.categories = await GetCategoriesAsync();

            ProductUpdateVM model = new()
            {
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Description = product.Description,
                Images = product.Images //sehv yoldur
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ProductUpdateVM model)
        {
            try
            {
                if (id is null) return BadRequest();
                Product dbProduct = await _productService.GetFullDataByIdAsync((int)id);
                if (dbProduct is null) return NotFound();

                List<ProductImage> productImages = new();

                foreach (var item in dbProduct.Images)
                {
                    ProductImage image = new()
                    {
                        Image = item.Image
                    };
                    productImages.Add(image);
                }
                ProductUpdateVM product = new()
                {
                    Name = dbProduct.Name,
                    Price = dbProduct.Price,
                    CategoryId = dbProduct.CategoryId,
                    Description = dbProduct.Description,
                    Images = productImages
                };

                ViewBag.categories = await GetCategoriesAsync();

                if (!ModelState.IsValid) return View(product);

                int canUploadImg = 5 - (int)product.Images?.Where(i => !i.SoftDelete).Count();

                if (model.Photos is not null)
                {
                    if (model.Photos.Count() > canUploadImg)
                    {
                        ModelState.AddModelError("Photos", $"The maximum number of images you can upload is {canUploadImg}");
                        return View(product);
                    }
                    foreach (var photo in model.Photos)
                    {
                        if (!photo.CheckFileType("image/"))
                        {
                            ModelState.AddModelError("Photo", "File type must be image");
                            return View(product);
                        }
                        if (!photo.CheckFileSize(200))
                        {
                            ModelState.AddModelError("Photo", "Image size must be max 200kb");
                            return View(product);
                        }
                    }

                    foreach (var photo in model.Photos)
                    {
                        ProductImage productImage = new()
                        {
                            Image = photo.CreateFile(_env, "img")
                        };

                        dbProduct.Images.Add(productImage);
                    }
                    dbProduct.Images.FirstOrDefault().IsMain = true;
                }
                else
                {
                    foreach (var item in dbProduct.Images)
                    {
                        ProductImage newProductImage = new()
                        {
                            Image = item.Image
                        };
                    }
                }

                dbProduct.Name = model.Name;
                dbProduct.Description = model.Description;
                dbProduct.Price = model.Price;
                dbProduct.CategoryId = model.CategoryId;

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id is null) return BadRequest();
            var image = _context.ProductImages.FirstOrDefault(pi => pi.Id == id);
            if (image is null) return NotFound();

            string path = FileHelper.GetFilePath(_env.WebRootPath, "img", image.Image);
            FileHelper.DeleteFile(path);
            _context.ProductImages.Remove(image);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SetStatus(int? id)
        {
            if (id == null) return BadRequest();
            var image = _context.ProductImages.FirstOrDefault(pi => pi.Id == id);

            if (image is null) return NotFound();

            image.IsMain = !image.IsMain;

            await _context.SaveChangesAsync();

            return Ok(image.IsMain);
        }


        public async Task<IActionResult> Detail(int? id)
        {

            if (id is null) return BadRequest();
            Product product = await _productService.GetFullDataByIdAsync((int)id);
            if (product is null) return NotFound();

            return View(product);


        }
    }
}
