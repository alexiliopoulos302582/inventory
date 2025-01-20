
using InventoryMonitor.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using InventoryMonitor.Models;


var builder = WebApplication.CreateBuilder(args);

// -------------------------------- Add services to the container.----------------------
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var cultureInfo = new CultureInfo("en-US")
{
    NumberFormat = { NumberDecimalSeparator = ".", CurrencyDecimalSeparator = "." }
};

// Set the default thread culture for the app
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Add localization to the request pipeline
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(cultureInfo);
    options.SupportedCultures = new List<CultureInfo> { cultureInfo };
    options.SupportedUICultures = new List<CultureInfo> { cultureInfo };
});




builder.Services.AddDbContext<SbodemoGbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Server=DESKTOP-1O36NRQ\\SQLEXPRESS;Database=SBODemoGB;Trusted_Connection=True;TrustServerCertificate=True;")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
