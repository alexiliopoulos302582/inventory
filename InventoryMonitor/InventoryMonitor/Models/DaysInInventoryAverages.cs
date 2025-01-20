namespace InventoryMonitor.Models;
    using System.ComponentModel.DataAnnotations;

    public class DaysInInventoryAverages
    {


        [Key] // Add this if "itemcode" is a unique identifier; otherwise, remove it
        
        public string ItemCode { get; set; }

       
        public string Description { get; set; }

        public int? Average { get; set; }

        public int? MinDays { get; set; }

        public int? MaxDays { get; set; }



    }
