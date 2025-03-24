using Pharmase.Data;
using Pharmase.Models;
using Microsoft.EntityFrameworkCore;
using Pharmase.ViewModels;
namespace Pharmase.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CreateCategoryAsync(CreateCategoryViewModel category)
        {
            Category cat = new()
            {
                Name = category.Name,
            };
            _context.Categories.Add(cat);
            _context.SaveChanges();

        }


        public bool DeleteCategory(int id)
        {
            var cat = _context.Categories.Find(id);

            if (cat is null)
            {
                return false;
            }
            _context.Categories.Remove(cat);
            _context.SaveChanges();

            return true;

        }


    }
}
