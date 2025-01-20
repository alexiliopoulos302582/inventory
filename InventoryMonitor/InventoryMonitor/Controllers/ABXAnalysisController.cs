using InventoryMonitor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryMonitor.Controllers
{
    public class ABXAnalysisController : Controller
    {

        private readonly SbodemoGbContext _context;

        public ABXAnalysisController(SbodemoGbContext context)
        {
            _context = context;
        }




        public async Task<IActionResult> GetFilteredAnalysis()
        {
            var filteredAnalysis = await _context.ABXFilteredAnalysis
                .Where(x => x.Category == "A" || x.Category == "B" || x.Category == "X")
                .ToListAsync();

            return View(filteredAnalysis);  // Returning to a view or returning data
        }



        public async Task<IActionResult> GetABXAnalysis(DateTime startDate, DateTime endDate)
        {
            // Ensure that the connection is established and active
            var results = await _context.ABXAnalysisResults
                                         .FromSqlRaw("EXEC dbo.GetABXFilteredAnalysis @StartDate = {0}, @EndDate = {1}", startDate, endDate)
                                         .ToListAsync();

            // You can now return the results to the view or process them as needed
            return View(results);
        }





    }
}
