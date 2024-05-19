using Microsoft.AspNetCore.Mvc;

namespace PrionaAlik.ViewComponets
{
    public class FooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
