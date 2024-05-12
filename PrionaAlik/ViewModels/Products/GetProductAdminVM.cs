using PrionaAlik.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrionaAlik.ViewModels.Products
{
    public class GetProductAdminVM
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostPrice { get; set; }
        public decimal SellPrice { get; set; }
        public int Discount { get; set; }
        public int StockCount { get; set; }
        public string ImageUrl { get; set; }
        public float Rating { get; set; }
    }
}
