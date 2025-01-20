namespace InventoryMonitor.Models
{
    public class CategorySummary
    {

        public string Category { get; set; } // Category name (e.g., A, B, C)
        public int ItemCount { get; set; } // Number of items in this category
        public decimal ContributionPercentage { get; set; } // Percentage contribution to the total
        public decimal ContributionValue { get; set; } // Value contribution (e.g., overall total * percentage)




    }
}
