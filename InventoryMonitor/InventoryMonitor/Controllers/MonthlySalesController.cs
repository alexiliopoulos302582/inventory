using Microsoft.AspNetCore.Mvc;


using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Mvc;
using InventoryMonitor.Models;

namespace InventoryMonitor.Controllers
{
    public class MonthlySalesController : Controller
    {

        private readonly SbodemoGbContext _context;

        // Constructor to inject the DbContext
        public MonthlySalesController(SbodemoGbContext context)
        {
            _context = context;
        }




        //public IActionResult Index()
        //{
        //    return View();
        //}




        // GET: MonthlySales
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            // Check if dates are provided; if not, show the current data
            if (startDate.HasValue && endDate.HasValue)
            {
                // Call stored procedure to update MonthlySales table with the dates passed from the form
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.UpdateMonthlySales_Alexandros @StartDate = {0}, @EndDate = {1}",
                    startDate.Value, endDate.Value
                );
            }

            // After executing the stored procedure, get the updated data
            var monthlySales = await _context.MonthlySales.ToListAsync();
            return View(monthlySales); // Pass data to the view
        }






        public IActionResult ProductSales()
        {
            return View(); // This renders the form to input the ItemCode
        }




        [HttpPost]
        public async Task<IActionResult> ProductSales(string itemCode)
        {
            if (string.IsNullOrEmpty(itemCode))
            {
                ViewBag.ErrorMessage = "Please enter a valid Item Code.";
                return View();
            }

            // Fetch the product's sales data
            var productSales = await _context.MonthlySales
                .Where(s => s.ItemCode == itemCode)
                .FirstOrDefaultAsync();

            if (productSales == null)
            {
                ViewBag.ErrorMessage = "No sales data found for the specified Item Code.";
                return View();
            }

            // Pass the sales data to a dedicated view for single product results
            return View("ProductSalesResult", productSales);
        }


        public class UpdateMonthlySalesRequest
        {
            public string ItemCode { get; set; }
            public decimal SafetyStock { get; set; }
            public decimal ReorderPoint { get; set; }
        }



        [HttpPost]
        public async Task<IActionResult> UpdateValues(string ItemCode,
            decimal ZFactor, decimal ExpectedServiceLevel, decimal SafetyStock, decimal ReorderPoint)
        {
            if (string.IsNullOrWhiteSpace(ItemCode))
            {
                return BadRequest("Invalid Item Code.");
            }

            try
            {
                // Log the received values for debugging
                //Console.WriteLine("Received Data:");
                //Console.WriteLine($"ItemCode: {ItemCode}");
                //Console.WriteLine($"ZFactor: {ZFactor}");
                //Console.WriteLine($"Expected Service Level: {ExpectedServiceLevel}");
                //Console.WriteLine($"SafetyStock: {SafetyStock}");
                //Console.WriteLine($"ReorderPoint: {ReorderPoint}");

                // Assuming your _context is correctly initialized
                var monthlySale = _context.MonthlySales.FirstOrDefault(m => m.ItemCode == ItemCode);
                if (monthlySale == null)
                {
                    return NotFound("Item not found.");
                }

                // Update the fields in your database model
                monthlySale.ServiceLevelFactorZ = ZFactor;
                monthlySale.ExpectedServiceLevel = ExpectedServiceLevel;
                monthlySale.SafetyStock = SafetyStock;
                monthlySale.ReorderPoint = ReorderPoint;

                // Save changes
                _context.SaveChanges();


                return await ProductSales(ItemCode);
                //RedirectToAction("Index", new { ItemCode = ItemCode }); // You can adjust the redirection as needed
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while updating values.");
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateEOQ(string itemCode, decimal? eoq)
        {
            // Retrieve the item from the database using the ItemCode
            var item = _context.MonthlySales.FirstOrDefault(i => i.ItemCode == itemCode);

            if (item == null)
            {
                // Handle case where the item is not found
                return NotFound();
            }

            // Update only the EOQ field
            item.EconomicOrderQuantity = eoq;

            // Save the updated item with the new EOQ
            _context.SaveChanges();

            // Return a success message or redirect to another page
            return await ProductSales(itemCode);
        }













    }
}
