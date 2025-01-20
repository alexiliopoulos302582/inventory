using Microsoft.AspNetCore.Mvc;




using System.Data;
using System.Data.SqlClient;
using InventoryMonitor.Models; // Ensure you have the necessary models namespace
using System.Collections.Generic;





namespace InventoryMonitor.Controllers
{
    public class XYZAnalysisController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        private readonly IConfiguration _configuration;

        public XYZAnalysisController(IConfiguration configuration)
        {
            _configuration = configuration;
        }




        [HttpPost]
        public IActionResult GetXYZAnalysis(DateTime startDate, DateTime endDate)
        {
            List<XYZAnalysisResult> results = new List<XYZAnalysisResult>();

            try
            {
                string connectionString1 = _configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection conn = new SqlConnection(connectionString1))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("XYZAnalysis_with_one_Query", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                results.Add(new XYZAnalysisResult
                                {
                                    ItemCode = reader["ItemCode"].ToString(),

                                    Description = reader["Dscription"].ToString(),

                                    StandardDeviation = reader["StandardDeviation"] == DBNull.Value
                                    ? null : (decimal?)Convert.ToDecimal(reader["StandardDeviation"]),

                                    AverageQuantity = reader["AverageQuantity"] == DBNull.Value
                                    ? null : (decimal?)Convert.ToDecimal(reader["AverageQuantity"]),

                                    CoefficientOfVariation = reader["CoefficientOfVariation"] == DBNull.Value
                                    ? null : (decimal?)Convert.ToDecimal(reader["CoefficientOfVariation"]),
                                    Characterization = reader["Characterization"].ToString()
                                });
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return BadRequest($"An error occurred: {ex.Message}");
            }
            return View("XYZAnalysisResults", results);

        }









    }
}
