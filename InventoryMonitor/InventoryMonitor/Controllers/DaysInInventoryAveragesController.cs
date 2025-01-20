using Microsoft.AspNetCore.Mvc;
using System.Linq;

using InventoryMonitor.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Microsoft.Data.SqlClient;

namespace InventoryMonitor.Controllers
{
    public class DaysInInventoryAveragesController : Controller
    { 


        private readonly SbodemoGbContext _context;




        public DaysInInventoryAveragesController(SbodemoGbContext context)
        {
            _context = context;
        }




        public IActionResult DaysInInventoryAverages()
        {
            var data = _context.DaysInInventoryAverages.ToList();
            return View(data); // Pass the data to the view
        }



        // Action to run the stored procedure
        [HttpPost]
        public IActionResult RunStoredProcedure(string ItemCode, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                // Define the SQL parameters
                var itemCodeParam = new SqlParameter("@ItemCode", SqlDbType.NVarChar, 50) { Value = ItemCode };
                var startDateParam = new SqlParameter("@StartDate", SqlDbType.DateTime) { Value = StartDate };
                var endDateParam = new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = EndDate };

                // Execute the stored procedure
                _context.Database.ExecuteSqlRaw("EXEC [dbo].[CalculateFIFOInventory] @ItemCode, @StartDate, @EndDate",
                    itemCodeParam, startDateParam, endDateParam);

                // Reload the data (optional, if the stored procedure modifies the table)
                var updatedData = _context.DaysInInventoryAverages.ToList();

                // Pass updated data to the view
                return View("DaysInInventoryAverages", updatedData);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, display a message)
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("DaysInInventoryAverages");
            }
        }




        [HttpPost]
        public IActionResult RunStoredProcedureForAll(DateTime startDate, DateTime endDate)
        {
            try
            {
                // Run the stored procedure for all items
                _context.Database.ExecuteSqlRaw(
                    "EXEC CalculateFIFOInventoryForAllItems @StartDate, @EndDate",
                    new SqlParameter("@StartDate", startDate),
                    new SqlParameter("@EndDate", endDate)
                );

                TempData["SuccessMessage"] = "Procedure executed successfully for all items.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
            }

            // Reload the same view
            return RedirectToAction("DaysInInventoryAverages");
        }















    }
}
