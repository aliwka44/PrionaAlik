using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrionaAlik.DataAccesLayer;
using PrionaAlik.Models;

namespace PrionaAlik.Controllers
{
    public class ShopController : Controller
    {
        private readonly PrionaContext _context;

        public ShopController(PrionaContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 0, int? categoryId = null)
        {
            IQueryable<Product> query = _context.Products.Include(p => p.ProductCategories);


            if (categoryId != null)
            {

                query = query.Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId));
            }
            double max = query.Count();

            ViewBag.CurrentCategory = categoryId;
            ViewBag.MaxPage = Math.Ceiling((double)max / 3);
            ViewBag.CurrentPage = page + 1;
            query = query.Skip(3 * page).Take(3);

            //var prod=_context.Products.AsQueryable();

            ViewBag.Categories = await _context.Categories.Include(c => c.ProductCategories).ToListAsync();



            return View(await query.ToListAsync());
        }
    }
    }
