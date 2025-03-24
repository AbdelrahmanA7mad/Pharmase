using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmase.Data;
using Pharmase.Models;
using Pharmase.Services;
using Pharmase.ViewModels;
using System.Diagnostics;

namespace Pharmase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMedicineService _medicineService;
        private readonly AppDbContext _context;

        public HomeController(ICategoryService categoryService, IMedicineService medicineService,AppDbContext context)
        {
            _categoryService = categoryService;
            _medicineService = medicineService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Fetch total medicines count
            var totalMedicines = await _medicineService.GetMedicinesAsync();
            var totalMedicinesCount = totalMedicines.Count();

          //  var todaySales = await _orderService.GetTodaySales();

            var lowStockMedicines = await _medicineService.GetLowStockMedicines(10);

            var todaySales = await _context.Sales
            .Where(s => s.SaleDate.Date == DateTime.Today)
            .SumAsync(s => (decimal?)s.TotalPrice) ?? 0m;


            // var recentSales = await _orderService.GetRecentSales();

            var viewModel = new HomeViewModel
            {
                TotalMedicines = totalMedicinesCount,
                TodaySales = todaySales,
                LowStockMedicines = lowStockMedicines,
                RecentSales = []
            };

            return View(viewModel);
        }


    }
}
