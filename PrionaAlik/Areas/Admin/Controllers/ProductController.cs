using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrionaAlik.DataAccesLayer;
using PrionaAlik.Models;
using PrionaAlik.ViewModels.Products;
using PrionaAlik.Extensions;
using System.Text;

namespace Pronia.Areas.Admin.Controllers;
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
				StockCount = p.StockCount
			})
			.ToListAsync());
	}
	public async Task<IActionResult> Create()
	{
		ViewBag.Categories = await _context.Categories
			.Where(s => !s.IsDeleted)
			.ToListAsync();
		return View();
	}
	[HttpPost]
	public async Task<IActionResult> Create(CreateProductVM data)
	{
		if(!ModelState.IsValid)
			return View();


		if (data.ImageFile != null)
		{
			if (!data.ImageFile.IsValidType("image"))
				ModelState.AddModelError("ImageFile", "Fayl şəkil formatında olmalıdır.");
			if (!data.ImageFile.IsValidLength(300))
				ModelState.AddModelError("ImageFile", "Faylın ölçüsü 200kb-dan çox olmamalıdır.");
		}
		bool isImageValid = true;
		StringBuilder sb = new StringBuilder();
		foreach (var img in data.ImageFiles ?? new List<IFormFile>())
		{
			if (!img.IsValidType("image"))
			{
				sb.Append("-" + img.FileName + " faylı şəkil formatında olmalıdır.");
				isImageValid = false;
			}
			if (!img.IsValidLength(300))
			{
				sb.Append("-" + img.FileName + " faylının ölçüsü 300kb-dan çox olmamalıdır.");
				isImageValid = false;
			}
		}
		if (!isImageValid)
		{
			ModelState.AddModelError("ImageFiles", sb.ToString());
		}
		if (await _context.Categories.CountAsync(c => data.CategoryIds.Contains(c.Id)) != data.CategoryIds.Length)

			ModelState.AddModelError("CategoryIds", "kateqori tapilmadi");
		if (!ModelState.IsValid)
		{
			ViewBag.Categories = await _context.Categories
			.Where(s => !s.IsDeleted).ToListAsync();
			return View(data);

		}
		ViewBag.Categories = await _context.Categories
			.Where(s => !s.IsDeleted).ToListAsync();
		return View(data);
		string fileName = await data.ImageFile.SaveFileAsync(Path.Combine(_env.WebRootPath, "imgs", "products"));
		Product prod = new Product
		{
			CostPrice = data.CostPrice,
			CreatedTime = DateTime.Now,
			Discount = data.Discount,
			ImageUrl = Path.Combine("imgs", "products", fileName),
			IsDeleted = false,
			Name = data.Name,
			Rating = data.Rating,
			SellPrice = data.SellPrice,
			StockCount = data.StockCount,
		//	Images = new List<ProductImage>(),
			//ProductCategories = data.CategoryIds.Select(x => new 
		//		ProductCategory{
		//		CategoryId=x
			
		//	}).ToList()
			};
		foreach (var img in data.ImageFiles)
		{
			string imgName = await img.SaveFileAsync(Path.Combine(_env.WebRootPath, "imgs", "products"));
			//await _context.ProductImages.AddAsync(new ProductImage
			//{
			//    ImageUrl = Path.Combine("imgs", "products", imgName),
			//    CreatedTime = DateTime.Now,
			//    IsDeleted = false,
			//    Product = prod
			//})
			prod.Images.Add(new ProductImage
			{
				ImageUrl = Path.Combine("imgs", "products", imgName),
				CreatedTime = DateTime.Now,
				IsDeleted = false,
			});
		}
		await _context.Products.AddAsync(prod);
		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}
}