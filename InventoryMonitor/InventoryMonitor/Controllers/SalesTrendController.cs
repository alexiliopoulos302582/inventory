using Microsoft.AspNetCore.Mvc;



using System.Data.SqlClient;
using System.Data;

using System.Linq;
using System.Collections.Generic;
namespace InventoryMonitor.Controllers
{
    public class SalesTrendController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private string connectionString = "Server=DESKTOP-1O36NRQ\\SQLEXPRESS;Database=SBODemoGB;User Id=sa;Password=admin;TrustServerCertificate=True;";





        [HttpPost]
        public IActionResult GetTrendData([FromBody] ProductRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.ProductCode))
                return BadRequest("Product code is required.");

            // Fetch all data from the view
            var allSalesData = GetAllSalesData();

            // Filter by the specified product code
            var filteredSalesData = allSalesData
                .Where(s => s.ItemCode.Equals(request.ProductCode, StringComparison.OrdinalIgnoreCase))
                .OrderBy(s => s.DocumentDate) // Ensure data is ordered by date
                .ToList();

            if (!filteredSalesData.Any())
                return NotFound("No data found for the specified product code.");

            // Perform regression
            var regressionResult = PerformRegression(filteredSalesData);

            return Json(regressionResult);
        }

        private List<SalesData> GetAllSalesData()
        {
            var salesData = new List<SalesData>();

            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT [Item Code], [Document Date], [Net Quantity] FROM NetQuantityPerItemAndDate";
                var command = new SqlCommand(query, connection);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        salesData.Add(new SalesData
                        {
                            ItemCode = reader["Item Code"].ToString(),
                            DocumentDate = Convert.ToDateTime(reader["Document Date"]),
                            NetQuantity = Convert.ToDouble(reader["Net Quantity"])
                        });
                    }
                }
            }

            return salesData;
        }

        private RegressionResult PerformRegression(List<SalesData> salesData)
        {
            // Convert dates to numerical format for regression
            var xData = salesData.Select((s, index) => (double)index).ToArray(); // Use index as proxy for time
            var yData = salesData.Select(s => s.NetQuantity).ToArray();

            // Perform linear regression: y = mx + c
            double xMean = xData.Average();
            double yMean = yData.Average();

            double numerator = 0;
            double denominator = 0;
            for (int i = 0; i < xData.Length; i++)
            {
                numerator += (xData[i] - xMean) * (yData[i] - yMean);
                denominator += Math.Pow(xData[i] - xMean, 2);
            }

            double m = numerator / denominator; // Slope
            double c = yMean - m * xMean;       // Intercept

            // Trendline formula
            string formula = $"y = {m:F2}x + {c:F2}";

            return new RegressionResult
            {
                Formula = formula,
                DataPoints = salesData.Select((s, index) => new { X = index, Y = s.NetQuantity }).ToList()
            };
        }

        public class SalesData
        {
            public string ItemCode { get; set; }
            public DateTime DocumentDate { get; set; }
            public double NetQuantity { get; set; }
        }

        public class RegressionResult
        {
            public string Formula { get; set; }
            public object DataPoints { get; set; }
        }

        public class ProductRequest
        {
            public string ProductCode { get; set; }
        }





    }
}
