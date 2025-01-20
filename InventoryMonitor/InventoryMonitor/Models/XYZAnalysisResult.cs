namespace InventoryMonitor.Models
{
    public class XYZAnalysisResult
    {



        public string ItemCode { get; set; }
        public string Description { get; set; }
        public decimal? StandardDeviation { get; set; }
        public decimal? AverageQuantity { get; set; }
        public decimal? CoefficientOfVariation { get; set; }
        public string Characterization { get; set; }





    }
}
