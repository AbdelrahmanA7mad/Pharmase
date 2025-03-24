using Microsoft.AspNetCore.Mvc;
using Pharmase.Services;
using Pharmase.ViewModels;

namespace Pharmase.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IMedicineService _medicineService;
        private readonly ICategoryService _categoryService;

        public InventoryController(IMedicineService meService, ICategoryService categoryService)
        {
            _medicineService = meService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var medicines = await _medicineService.GetMedicinesAsync();
            var categories = await _categoryService.GetCategoriesAsync();

            var viewModel = new InventoryViewModel
            {
                medicines = medicines,
                Categories = categories
            };

            return View(viewModel);
        }
    }
}
