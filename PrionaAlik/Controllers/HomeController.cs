using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrionaAlik.DataAccesLayer;
using PrionaAlik.ViewModels.Categories;
using PrionaAlik.ViewModels.Defaults;
using PrionaAlik.ViewModels.Sliders;


namespace PrionaAlik.Controllers
{
    public class HomeController : Controller
    {
        private readonly PrionaContext _context;
        public HomeController(PrionaContext context) {
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
              var sliders=    await _context.Sliders
                .Where(x=>!x.IsDeleted)
                .Select(s => new GetSliderVM
            {
                Discount = s.Discount,
                Id = s.Id,
                ImageUrl = s.ImageUrl,
                Subtitle = s.Subtitle,
                Title = s.Title,
            }).ToListAsync();
            var categories= await _context.Categories
                 .Where(x => !x.IsDeleted)
                .Select(x=> new GetCategoryVM
                {
                    Id= x.Id,
                    Name= x.Name,
                }).ToListAsync();
            return View(new HomeVM
            {
                Categories = categories,
                Sliders = sliders
            });

            }
        //public async Task<IActionResult> Test(int? id)
        //{
        //    //if (string.IsNullOrWhiteSpace(name)) return BadRequest();
        //    // Category cat = new Category
        //    // {
        //    //     Name = name,
        //    //     CreatedTime = DateTime.Now,
        //    //     IsDeleted = false
        //    // };
        //    //await _context.Categories.AddAsync(cat);
        //    //    await _context.SaveChangesAsync();
        //    if (id == null || id < 1) return BadRequest();
        //    var cat = await _context.Categories.FindAsync(id);
        //    if (cat == null) return NotFound();
        //    _context.Remove(cat);
        //    await _context.SaveChangesAsync();
        //    return Content(cat.Name);
        //}

			public async Task<IActionResult> Contact()
            {
                return View();
            }
        }
    } 
