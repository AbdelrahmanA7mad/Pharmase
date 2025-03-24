using Microsoft.AspNetCore.Mvc;
using Pharmase.Models;
using Pharmase.Services;
using Pharmase.ViewModels;

namespace Pharmase.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CreateCategoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }
            await _categoryService.CreateCategoryAsync(viewModel);

            return RedirectToAction("Index", "Inventory");
        }

        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            var isdeleted = _categoryService.DeleteCategory(id);
            return isdeleted ? Ok() : BadRequest();
        }

    }
}

