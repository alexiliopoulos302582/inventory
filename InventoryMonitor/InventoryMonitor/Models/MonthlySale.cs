namespace InventoryMonitor.Models
{
    public class MonthlySale
    {

        public string ItemCode { get; set; } = null!;

        public decimal? AvgLeadTimeinDays { get; set; }

        public decimal? January { get; set; }

        public decimal? February { get; set; }

        public decimal? March { get; set; }

        public decimal? April { get; set; }

        public decimal? May { get; set; }

        public decimal? June { get; set; }

        public decimal? July { get; set; }

        public decimal? August { get; set; }

        public decimal? September { get; set; }

        public decimal? October { get; set; }

        public decimal? November { get; set; }

        public decimal? December { get; set; }

        public decimal? StandardCost { get; set; }

        public decimal? OrderingFrequencyPerMonth { get; set; }

        public int? Year { get; set; }

        public decimal? YearlyQuantityDemand { get; set; }

        public decimal? AverageMonthlyDemand { get; set; }

        public decimal? AverageDailyDemand { get; set; }

        public decimal? StandardDeviationOfMonthlyDemand { get; set; }

        public decimal? CoefficientOfVariance { get; set; }

        public decimal? AverageLeadTimeInMonths { get; set; }

        public decimal? ExpectedServiceLevel { get; set; }

        public decimal? ServiceLevelFactorZ { get; set; }

        public decimal? SafetyStock { get; set; }

        public decimal? ReorderPoint { get; set; }

        public decimal? EconomicOrderQuantity { get; set; }



    }
}
