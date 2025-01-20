using Microsoft.AspNetCore.Mvc;
using System.Data;

using Microsoft.AspNetCore.Mvc;
using InventoryMonitor.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;



namespace InventoryMonitor.Controllers
{
    public class ABCAnalysisController : Controller
    {

        private readonly IConfiguration _configuration;
        private SbodemoGbContext _context;


        public IActionResult Index()
        {
            return View();
        }

        public ABCAnalysisController(IConfiguration configuration , SbodemoGbContext context)
        {
            _configuration = configuration;
            _context = context;
        }


        private string connectionString = "Server=DESKTOP-1O36NRQ\\SQLEXPRESS;Database=SBODemoGB;User Id=sa;Password=admin;TrustServerCertificate=True;";

        [HttpPost]
        public ActionResult GetABCAnalysis(DateTime startDate, DateTime endDate)
        {

            List<ABCAnalysisResult> results = new List<ABCAnalysisResult>();
            List<CategorySummary> categorySummary = new List<CategorySummary>();
            decimal overallTotalValue = 0; // Variable to store the overall total value

            string connectionString1 = _configuration.GetConnectionString("DefaultConnection");
            try
            {
                // Get the connection string from appsettings.json
                
                using (SqlConnection conn = new SqlConnection(connectionString1))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetABCAnalysis", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new ABCAnalysisResult
                                {
                                    ItemCode = reader["ItemCode"].ToString(),
                                    SalesYear = Convert.ToInt32(reader["SalesYear"]),
                                    TotalValue = Convert.ToDecimal(reader["TotalValue"]),
                                    CumulativeValue = Convert.ToDecimal(reader["CumulativeValue"]),
                                    CumulativePercentage = Convert.ToDecimal(reader["CumulativePercentage"]),
                                    Category = reader["Category"].ToString()
                                };
                                results.Add(item);
                                overallTotalValue += item.TotalValue;
                            }
                        }
                    }

                    // for the cumulative table at the bottom of the page----------------------------------
                    // Step 2: Calculate the total number of items
                    int totalItemCount = results.Count;

                    // Step 3: Group by category and calculate summary        
                    var categoryCounts = results.GroupBy(x => x.Category) //Group the list by the 'Category' property
                        .Select(x => new                        //Transform each group into a new object
                        {
                            Category = x.Key,                       //The group key (the 'Category' value)
                            ItemCount = x.Count(),              //The number of items in this group
                            TotalValue = x.Sum(r => r.TotalValue)  // The sum of 'TotalValue' for this group
                        });

                   


                    categorySummary = categoryCounts.Select(c => new CategorySummary
                    {
                        Category = c.Category,
                        ItemCount = c.ItemCount,
                        ContributionPercentage = ((decimal)c.ItemCount / totalItemCount) * 100,
                        // Percentage based on item count
                        ContributionValue = overallTotalValue * ((decimal)c.ItemCount / totalItemCount)
                        // Multiply overall total value by percentage

                    }).ToList();

                }
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = "An error occurred while fetching data: " + ex.Message;
                return View("Error");
            }
            //-------------------  Pass both datasets to the view----------------
            var model = new ABCAnalysisViewModel
            {
                ItemResults = results,
                CategorySummary = categorySummary,
                OverallTotalValue = overallTotalValue
            };
            return View("ABCAnalysisView", model);



        }





       

       




    }
}
