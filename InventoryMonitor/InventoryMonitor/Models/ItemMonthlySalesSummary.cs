namespace InventoryMonitor.Models
{
    public class ItemMonthlySalesSummary
    {



        public string ItemCode { get; set; }
        public int Year { get; set; }
        public int TotalQuantity { get; set; }
        public decimal MeanQuantity { get; set; }
        public decimal StdDevQuantity { get; set; }
        public decimal CoefficientOfVariation { get; set; }




    }
}
