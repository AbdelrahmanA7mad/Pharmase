using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmase.Data;
using Pharmase.ReportViewModel;
using System.Text;

namespace Pharmase.Controllers
{
    public class ReportsController : Controller
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Reports
        public IActionResult Index()
        {
            return View();
        }

        // Sales Report
        // Controller
        public async Task<IActionResult> SalesReport(DateTime? startDate, DateTime? endDate)
        {
            // Validate date range
            if (endDate < startDate)
            {
                ModelState.AddModelError("", "End date cannot be before start date");
                return View(new SalesReportViewModel());
            }

            var salesQuery = _context.Sales.AsQueryable();

            // Apply date filters
            if (startDate.HasValue)
                salesQuery = salesQuery.Where(s => s.SaleDate >= startDate.Value.Date);

            if (endDate.HasValue)
                salesQuery = salesQuery.Where(s => s.SaleDate <= endDate.Value.Date.AddDays(1).AddTicks(-1));

            var sales = await salesQuery.ToListAsync();

            // Create daily sales data
            var dailySales = sales
                .GroupBy(s => s.SaleDate.Date)
                .Select(g => new DailySales
                {
                    Date = g.Key,
                    TotalAmount = g.Sum(s => s.TotalPrice),
                    TransactionCount = g.Count()
                })
                .OrderBy(d => d.Date)
                .ToList();

            var reportData = new SalesReportViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                TotalSales = sales.Sum(s => s.TotalPrice),
                AverageSale = sales.Any() ? sales.Average(s => s.TotalPrice) : 0,
                TotalTransactions = sales.Count,
                DailySales = dailySales,
                HighestDailySale = dailySales.Any() ? dailySales.Max(d => d.TotalAmount) : 0,
                LowestDailySale = dailySales.Any() ? dailySales.Min(d => d.TotalAmount) : 0
            };

            // Set default dates if not provided
            if (!startDate.HasValue || !endDate.HasValue)
            {
                reportData.StartDate = dailySales.FirstOrDefault()?.Date;
                reportData.EndDate = dailySales.LastOrDefault()?.Date;
            }

            return View(reportData);
        }
        public async Task<IActionResult> InventoryReport()
        {
            var inventoryData = await _context.Medicines
                .Select(m => new InventoryReportItem
                {
                    MedicineName = m.Name,
                    Category = m.Category != null ? m.Category.Name : "N/A", // Handle null Category
                    CurrentStock = m.StockQuantity,
                    ExpiryDate = m.ExpiryDate,
                })
                .OrderBy(m => m.Category)
                .ThenBy(m => m.MedicineName)
                .ToListAsync();

            return View(inventoryData);
        }
        // Export Sales Report as CSV
        //public async Task<IActionResult> ExportSalesReport(DateTime? startDate, DateTime? endDate)
        //{
        //    // Validate date range
        //    if (endDate < startDate)
        //    {
        //        ModelState.AddModelError("", "End date cannot be before start date");
        //        return View(new SalesReportViewModel());
        //    }

        //    var salesQuery = _context.Sales.AsQueryable();

        //    // Apply date filters
        //    if (startDate.HasValue)
        //        salesQuery = salesQuery.Where(s => s.SaleDate >= startDate.Value.Date);

        //    if (endDate.HasValue)
        //        salesQuery = salesQuery.Where(s => s.SaleDate <= endDate.Value.Date.AddDays(1).AddTicks(-1));

        //    var sales = await salesQuery.ToListAsync();

        //    // Create daily sales data
        //    var dailySales = sales
        //        .GroupBy(s => s.SaleDate.Date)
        //        .Select(g => new DailySales
        //        {
        //            Date = g.Key,
        //            TotalAmount = g.Sum(s => s.TotalPrice),
        //            TransactionCount = g.Count()
        //        })
        //        .OrderBy(d => d.Date)
        //        .ToList();

        //    // Prepare CSV content
        //    var csv = new StringBuilder();
        //    csv.AppendLine("Date,Total Sales,Average Sale,Transactions");

        //    foreach (var sale in dailySales)
        //    {
        //        csv.AppendLine($"{sale.Date:yyyy-MM-dd},{sale.TotalAmount},{(sales.Any() ? sales.Average(s => s.TotalPrice) : 0)},{sale.TransactionCount}");
        //    }

        //    // Add overall totals to CSV
        //    csv.AppendLine($"Total,{sales.Sum(s => s.TotalPrice)},{(sales.Any() ? sales.Average(s => s.TotalPrice) : 0)},{sales.Count}");


        //    // Return the CSV file for download
        //    var fileName = "SalesReport.csv";
        //    var content = Encoding.UTF8.GetBytes(csv.ToString());
        //    return File(content, "text/csv", fileName);
        //}

    }
}
