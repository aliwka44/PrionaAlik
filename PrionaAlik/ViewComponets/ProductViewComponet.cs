using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrionaAlik.DataAccesLayer;
using PrionaAlik.ViewModels.Products;

namespace PrionaAlik.ViewComponets
{
    public class ProductViewComponent(PrionaContext _context) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _context.Products.Select(p => new GetProductVM
            {
                Id = p.Id,
                Name = p.Name,
                Discount = p.Discount,
                ImageUrl = p.ImageUrl,
                IsStock = p.StockCount > 0,
                Rating = p.Rating,
                SellPrice = p.SellPrice
            }).ToListAsync());
        }
    }
}
