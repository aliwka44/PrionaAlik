using System.ComponentModel.DataAnnotations;

namespace PrionaAlik.ViewModels.Products
{
	public class CreateProductVM
    {
        public string Name { get; set; }
        [Range(0,2000)]
        public decimal CostPrice { get; set; }
        public decimal SellPrice { get; set; }
        public int Discount { get; set; }
        public int StockCount { get; set; }
        public float Rating { get; set; }
        public int[] CategoryIds { get; set; }
        public IFormFile ImageFile { get; set; }
        public IEnumerable<IFormFile> ImageFiles { get; set; }

    }
}
