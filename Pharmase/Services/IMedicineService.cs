using Pharmase.Models;
using Pharmase.ViewModels;

namespace Pharmase.Services
{
    public interface IMedicineService
    {
        Task<List<Medicine>> GetMedicinesAsync();
        Task<Medicine> GetMedicineByIdAsync(int id);
        Task CreateMedicineAsync(CreateMedicineViewModel model);
        Task<bool> UpdateMedicineAsync(int id, CreateMedicineViewModel model);
        bool DeleteProduct(int id);
        Task<IEnumerable<Medicine>> GetLowStockMedicines(int threshold);
    }
}
