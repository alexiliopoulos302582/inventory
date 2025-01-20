namespace InventoryMonitor.Models
{
   
     

    public partial class InventoryKpi
    {
        public string ItemCode { get; set; } = null!;

        public decimal? StandardCostPerUnit { get; set; }

        public decimal? AverageSalesPricePerUnit { get; set; }

        //public decimal? StockOnHand { get; set; }

        public int? Year { get; set; }

        public decimal? InventoryAtBeginningOfPeriod { get; set; }

        public decimal? InventoryAtEndOfPeriod { get; set; }

        public decimal? NumberOfGoodsSoldYearly { get; set; }

        public decimal? AverageDailySold { get; set; }

        public decimal? CostOfGoodsSold { get; set; }

        public decimal? Sales { get; set; }

        public decimal? GrossMargin { get; set; }

        public decimal? AverageInventory { get; set; }

        public decimal? InventoryTurns { get; set; }

        public decimal? DaysOnHand { get; set; }

        //public decimal? DaysOfSales { get; set; }

        public decimal? Gmroi { get; set; }
    }





}
 
