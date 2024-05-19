using Microsoft.AspNetCore.Mvc;
using PrionaAlik.DataAccesLayer;

namespace PrionaAlik.ViewComponets
{
    public class HeaderViewComponent(PrionaContext _context) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
