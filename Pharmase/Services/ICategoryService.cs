using Pharmase.Models;
using Pharmase.ViewModels;

namespace Pharmase.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task CreateCategoryAsync(CreateCategoryViewModel category);
        bool DeleteCategory(int id);
    }
}
