using Microsoft.AspNetCore.Mvc;
using Pharmase.Data;
using Pharmase.Models;
using Pharmase.Services;
using Pharmase.ViewModels;

namespace Pharmase.Controllers
{
    public class MedicineController : Controller
    {
        private readonly IMedicineService _medicineService;
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _db;

        public MedicineController(IMedicineService medicineService, ICategoryService categoryService ,AppDbContext db)
        {
            _medicineService = medicineService;
            _categoryService = categoryService;
            _db = db;
        }

        // GET: Medicine
        public async Task<IActionResult> Index()
        {
            var medicines = await _medicineService.GetMedicinesAsync();
            return View(medicines);
        }

        // GET: Medicine/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var medicine = await _medicineService.GetMedicineByIdAsync(id);
            if (medicine == null)
            {
                return NotFound();
            }
            return View(medicine);
        }

        // GET: Medicine/Create
        public async Task<IActionResult> Create()
        {
            var categories = _db.Categories.ToList();
            CreateMedicineViewModel viewModel = new()
            {
                Categories = _db.Categories.
                Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList()

            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMedicineViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _db.Categories.
                Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();

                return View(model);

            }
            await _medicineService.CreateMedicineAsync(model);

            return RedirectToAction("Index", "Inventory");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var medicine = await _medicineService.GetMedicineByIdAsync(id);
            if (medicine == null)
            {
                return NotFound();
            }

            // Map Medicine to CreateMedicineViewModel
            var model = new CreateMedicineViewModel
            {
                Id = medicine.Id,
                Name = medicine.Name,
                Price = medicine.UnitPrice,
                ExpireDate = medicine.ExpiryDate,
                Stock = medicine.StockQuantity,
                CategoryId = medicine.CategoryId,
                Categories = _db.Categories.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };

            return View(model);
        }

        // POST: Medicine/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateMedicineViewModel model)
        {
            if (id != model.Id) // Ensure the id matches the one in the model
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                model.Categories = _db.Categories
                    .Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .ToList();

                return View(model);
            }

            var isUpdated = await _medicineService.UpdateMedicineAsync(id, model);

            if (!isUpdated)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Inventory");
        }
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            var isdeleted = _medicineService.DeleteProduct(id);
            return isdeleted ? Ok() : BadRequest();
        }
    }
}

