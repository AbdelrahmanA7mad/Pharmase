using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using Pharmase.Data;
using Pharmase.Models;
using Pharmase.ReportViewModel;
using Pharmase.ViewModels;
using System.Drawing.Imaging;
using System.Drawing.Printing;

namespace Pharmase.Controllers
{
    public class SaleController : Controller
    {
        private readonly AppDbContext _context;

        public SaleController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sale/Create
        [HttpPost]
        public IActionResult Create(SaleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // Create the Sale
                        var sale = new Sale
                        {
                            SaleDate = DateTime.Now, // Set the sale date to the current date
                            TotalPrice = 0 
                        };

                        _context.Sales.Add(sale);
                        _context.SaveChanges();

                        decimal totalPrice = 0;

                        // Add Sale Items
                        foreach (var saleItem in viewModel.SaleItems)
                        {
                            var medicine = _context.Medicines.Find(saleItem.MedicineId);
                            if (medicine == null || medicine.StockQuantity < saleItem.QuantitySold)
                            {
                                ModelState.AddModelError("", $"الكمية غير متوفرة لـ {medicine?.Name ?? "الدواء"}");
                                transaction.Rollback();
                                return View(viewModel);
                            }

                            var saleItemEntity = new SaleItem
                            {
                                SaleId = sale.Id,
                                MedicineId = saleItem.MedicineId,
                                QuantitySold = saleItem.QuantitySold,
                                Price = medicine.UnitPrice // Use the price from the database
                            };

                            totalPrice += saleItemEntity.QuantitySold * saleItemEntity.Price;

                            // Update Medicine Stock
                            medicine.StockQuantity -= saleItemEntity.QuantitySold;
                            _context.Medicines.Update(medicine);

                            _context.SaleItems.Add(saleItemEntity);
                        }

                        // Update Total Price
                        sale.TotalPrice = totalPrice;
                        _context.SaveChanges();
                        transaction.Commit();

                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ModelState.AddModelError("", "حدث خطأ أثناء الحفظ: " + ex.Message);
                        return View(viewModel);
                    }
                }
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Index()
        {
            var sales = await _context.Sales
                .Include(s => s.SaleItems)
                    .ThenInclude(si => si.Medicine)
                .OrderByDescending(s => s.SaleDate)
                .Take(5)
                .ToListAsync();

            var todaySales = await _context.Sales
                .Where(s => s.SaleDate.Date == DateTime.Today)
                .SumAsync(s => (decimal?)s.TotalPrice) ?? 0m;

            var totalOrders = await _context.Sales
                .Where(s => s.SaleDate.Date == DateTime.Today)
                .CountAsync();

            return View(new SaleIndexViewModel
            {
                Sales = sales,
                TodaySales = todaySales,
                TotalOrders = totalOrders
            });
        }
        [HttpGet]
        public IActionResult GetMedicines()
        {
            var medicines = _context.Medicines
                                    .Select(m => new { m.Id, m.Name, m.UnitPrice })
                                    .ToList();

            return Json(medicines);
        }



        public async Task<IActionResult> Details(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.SaleItems)
                    .ThenInclude(si => si.Medicine)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }
    }
}
