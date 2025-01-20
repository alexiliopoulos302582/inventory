namespace InventoryMonitor.Models
{
    public class ABCAnalysisResult
    {


        public string ItemCode { get; set; }
        public int SalesYear { get; set; }
        public decimal TotalValue { get; set; }
        public decimal CumulativeValue { get; set; }
        public decimal CumulativePercentage { get; set; }
        public string Category { get; set; }




    }
}
