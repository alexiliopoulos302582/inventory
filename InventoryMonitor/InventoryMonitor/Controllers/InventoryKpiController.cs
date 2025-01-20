using InventoryMonitor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryMonitor.Controllers
{
    public class InventoryKpiController : Controller
    {



        private readonly SbodemoGbContext _context;


        public InventoryKpiController(SbodemoGbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }







        [HttpGet]
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                // Call the stored procedure
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC UpdateInventoryKPIs @StartDate = {0}, @EndDate = {1}",
                    startDate.Value, endDate.Value);

                // Optional: You can also show a success message using TempData
                TempData["Message"] = "Inventory KPIs updated successfully!";
            }
            else
            {
                // Optional: Set a message if dates are not provided
                TempData["ErrorMessage"] = "Please provide both start and end dates.";
            }

            // Return the updated Inventory KPI list
            return View(await _context.InventoryKpis.ToListAsync());
        }






        // GET: InventoryKpi/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryKpi = await _context.InventoryKpis
                .FirstOrDefaultAsync(m => m.ItemCode == id);
            if (inventoryKpi == null)
            {
                return NotFound();
            }

            return View(inventoryKpi);
        }




        // POST: Inventory/UpdateBalances
        [HttpPost]
        public async Task<IActionResult> UpdateBalances(int year)
        {
            try
            {
                // Generate start and end dates based on the provided year
                // Call the stored procedure with just the year as the parameter
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.UpdateInventoryOpen_Closing_Balances @Year = {0}",
                    year
                        );

                // Set the success message in TempData
                TempData["UpdateSuccessMessage"] = $"Balances successfully updated for the year {year}.";

                // Redirect to the same page, but include the fragment identifier to scroll to the target section
                return RedirectToAction("Index", "Home", new { id = "updateBalancesSection" });
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging (Optional)
                // You can log to a file or logging framework like Serilog, NLog, etc.
                // _logger.LogError(ex, "Error occurred while updating balances.");

                // Set the error message in TempData
                TempData["ErrorMessage"] = "An error occurred while updating balances: " + ex.Message;

                // Stay on the same page or redirect to a specific error page
                return RedirectToAction("Index", "InventoryKpi");
            }
        }





















    }
}
