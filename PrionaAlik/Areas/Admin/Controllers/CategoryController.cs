using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrionaAlik.DataAccesLayer;
using PrionaAlik.ViewModels.Categories;

namespace PrionaAlik.Areas.Admin.Controllers
{
    public class CategoryController(PrionaContext _sql) : Controller
    {
        [Area("Admin")]
        // GET: CategoryController
        public async Task<ActionResult> Index()
        {
            return View(await _sql.Categories.Select(c=> new GetCategoryVM{
                Id=c.Id,
                Name=c.Name,
            }).ToListAsync());
        }

        // GET: CategoryController/Details/5
       

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        // POST: CategoryController/Create
        [HttpPost]
        public async Task<ActionResult> Create( CreateCategoryVM vm)
        {
            if (vm.Name!= null&& !(await _sql.Categories.AnyAsync(c=> c.Name == vm.Name)))
            {
                ModelState.AddModelError("Name", "ad var");
            }
            if (!ModelState.IsValid)
            {
                return View(vm) ;
            }
            await _sql.Categories.AddAsync(new Models.Category
            {
                 CreatedTime = DateTime.Now,
                 IsDeleted = false,
                 Name = vm.Name,

            });
            await _sql.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
