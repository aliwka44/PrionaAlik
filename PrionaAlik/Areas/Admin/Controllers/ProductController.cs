

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PrionaAlik.DataAccesLayer;
using PrionaAlik.Extensions;
using PrionaAlik.Models;
using PrionaAlik.ViewModels.Products;
using System.Text;

namespace PrionaAlik.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController(PrionaContext _context, IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index()
        {

            return View(await _context.Products
                .Select(p => new GetProductAdminVM
                {
                    CostPrice = p.CostPrice,
                    Discount = p.Discount,
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Rating = p.Rating,
                    SellPrice = p.SellPrice,
                    StockCount = p.StockCount,

                })
                .ToListAsync());
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.
                Where(s=>!s.IsDeleted)
                .ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM data)
        {
            if(!data.ImageFile.IsValidType("image"))
                ModelState.AddModelError("ImageFil", "Fayil sekil formasinda olmalidi");

            foreach (IFormFile img in data.ImageFiles)
            {
                if (!img.IsValidLength(200))
                    if (!ModelState.IsValid) return View(data);
                return View(data);
            }
            bool isImageValid = true;
            StringBuilder sb = new StringBuilder();
            foreach (var img in data.ImageFiles)
            {
                if (img.IsValidType("image"))
                {
                    sb.Append("-" + img.FileName + " fayl sekil formatinda olamalidi" );
                    isImageValid = false;

                    // ModelState.AddModelError("image",img.FileName+ " fayl sekil formatinda olamalidi");

                }
                if (!img.IsValidLength(200))
                    {
                        sb.Append("-" + img.FileName + " olcusu -200" );
                    isImageValid = false;
                        //ModelState.AddModelError("imagefile", img.FileName+"fayilin olcusu 200den cox olmamalidi")
                    }
                if(!isImageValid)
                {
                    
                    ModelState.AddModelError("", sb.ToString());
                }
                
            }
            if (!ModelState.IsValid)
                return View(data);
            string fileName =await data.ImageFile.SaveFileAsync(Path.Combine(_env.WebRootPath,"imgs", "Products"));
            Product prod = new Product
            {
                CostPrice = data.CostPrice,
                CreatedTime = DateTime.Now,
                Discount = data.Discount,
                ImageUrl = Path.Combine("imgs", "Products", fileName),
                IsDeleted = false,
                Name = data.Name,
                Rating = data.Raiting,
                SellPrice = data.SellPrice,
                StockCount = data.StockCount,
                Images=new List<ProductImage>()
            };
            foreach (var img in data.ImageFiles)
            {
                string imgName = await img.SaveFileAsync(Path.Combine(_env.WebRootPath, "imgs", "Products"));
                prod.Images.Add(new ProductImage
                {
                    ImageUrl = Path.Combine("imgs", "Products", imgName),
                    CreatedTime= DateTime.Now,
                    IsDeleted = false,
                     
                });
            }
            await _context.Products.AddAsync(prod);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
    
}
