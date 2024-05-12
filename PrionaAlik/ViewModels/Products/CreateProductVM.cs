using System.ComponentModel.DataAnnotations.Schema;

namespace PrionaAlik.ViewModels.Products
{
    public class CreateProductVM
    {
        public string Name { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellPrice { get; set; }
        public int Discount { get; set; }
        public int StockCount { get; set; }
        public IFormFile ImageUrl { get; set; }
        public float Rating { get; set; }
        public IEnumerable<IFormFile> ImageFiles { get; set; }
    }
}
