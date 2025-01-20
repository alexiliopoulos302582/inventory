using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace InventoryMonitor.Models
{
    public partial class SbodemoGbContext : DbContext
    {

        public SbodemoGbContext()
        {
        }

        public SbodemoGbContext(DbContextOptions<SbodemoGbContext> options)
        : base(options)
        {
        }



        public virtual DbSet<InventoryKpi> InventoryKpis { get; set; }

        public virtual DbSet<MonthlySale> MonthlySales { get; set; }

        public DbSet<DaysInInventoryAverages> DaysInInventoryAverages { get; set; }

        public DbSet<ABXFilteredAnalysis> ABXFilteredAnalysis { get; set; }


        public DbSet<ABXAnalysisResult> ABXAnalysisResults { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

       => optionsBuilder.UseSqlServer("Server=DESKTOP-1O36NRQ\\SQLEXPRESS;Database=SBODemoGB;Trusted_Connection=True;TrustServerCertificate=True;");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP850_CI_AS");

            modelBuilder.Entity<InventoryKpi>(entity =>
            {
                entity.HasKey(e => e.ItemCode).HasName("PK__Inventor__3ECC0FEB4743D9D0");

                entity.ToTable("Inventory_KPIs");

                entity.Property(e => e.ItemCode).HasMaxLength(50);
                entity.Property(e => e.AverageDailySold).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.AverageInventory).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.AverageSalesPricePerUnit)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("AverageSalesPrice_per_Unit");
                entity.Property(e => e.CostOfGoodsSold).HasColumnType("decimal(18, 2)");
                //entity.Property(e => e.DaysOfSales).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.DaysOnHand).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Gmroi)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("GMROI");
                entity.Property(e => e.GrossMargin).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.InventoryAtBeginningOfPeriod).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.InventoryAtEndOfPeriod).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.InventoryTurns).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.NumberOfGoodsSoldYearly)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("NumberOfGoodsSold_Yearly");
                entity.Property(e => e.Sales).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.StandardCostPerUnit)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("StandardCost_per_Unit");
                //entity.Property(e => e.StockOnHand).HasColumnType("decimal(18, 2)");
            });


            modelBuilder.Entity<MonthlySale>(entity =>
            {
                entity.HasKey(e => e.ItemCode).HasName("PK__MonthlyS__3ECC0FEB51092732");

                entity.Property(e => e.ItemCode).HasMaxLength(50);
                entity.Property(e => e.April)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.August)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.AverageDailyDemand)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.AverageLeadTimeInMonths)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.AverageMonthlyDemand)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.AvgLeadTimeinDays).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.CoefficientOfVariance)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.December)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.EconomicOrderQuantity)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.ExpectedServiceLevel)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.February)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.January)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.July)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.June)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.March)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.May)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.November)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.October)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.OrderingFrequencyPerMonth).HasColumnType("decimal(18, 2)")
            .HasDefaultValue(0.00m); // Default value must be decimal, not int
                entity.Property(e => e.ReorderPoint)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.SafetyStock)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.September)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.ServiceLevelFactorZ)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("ServiceLevelFactor(Z)");
                entity.Property(e => e.StandardCost)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.StandardDeviationOfMonthlyDemand)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Year).HasDefaultValue(0);
                entity.Property(e => e.YearlyQuantityDemand)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(18, 2)");
            });



            modelBuilder.Entity<DaysInInventoryAverages>(entity =>
            {
                entity.ToTable("DaysInInventoryAverages"); // Maps to the SQL table name

                entity.Property(e => e.ItemCode)
                    .HasMaxLength(50)
                    .IsUnicode(true); // Matches NVARCHAR

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(true);

                entity.Property(e => e.Average)
                    .HasColumnType("int");

                entity.Property(e => e.MinDays)
                    .HasColumnType("int");

                entity.Property(e => e.MaxDays)
                    .HasColumnType("int");
            });

            modelBuilder.Entity<ABXAnalysisResult>().HasNoKey();

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}