using PrionaAlik.ViewModels.Categories;
using PrionaAlik.ViewModels.Sliders;

namespace PrionaAlik.ViewModels.Defaults
{
    public class HomeVM
    {
        public IEnumerable<GetSliderVM> Sliders { get; set; }
        public IEnumerable<GetCategoryVM> Categories { get; set; }
    }
}
