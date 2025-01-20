namespace InventoryMonitor.Models
{
    public class ABXFilteredAnalysis
    {



        public int Id { get; set; }  // Assuming the table has an Id column
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }  // For example: 'A', 'B', 'X'
        public decimal TotalValue { get; set; }




    }
}
