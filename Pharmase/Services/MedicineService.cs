using Pharmase.Data;
using Pharmase.Models;
using Microsoft.EntityFrameworkCore;
using Pharmase.ViewModels;

namespace Pharmase.Services
{
    public class MedicineService :IMedicineService
    {
        private readonly AppDbContext _context;

        public MedicineService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Medicine>> GetMedicinesAsync()
        {
            return await _context.Medicines.Include(m => m.Category).ToListAsync();
        }

        public async Task<Medicine> GetMedicineByIdAsync(int id)
        {
            return await _context.Medicines
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CreateMedicineAsync(CreateMedicineViewModel model)
        {
            Medicine med = new Medicine
            {
                Name = model.Name,
                UnitPrice = model.Price,
                ExpiryDate = model.ExpireDate,
                CategoryId = model.CategoryId,
                StockQuantity = model.Stock,
            };
            _context.Medicines.Add(med);   
            await _context.SaveChangesAsync();
        }
        public bool DeleteProduct(int id)
        {
            var product = _context.Medicines.Find(id);

            if (product is null)
            {
                return false; 
            }

            _context.Medicines.Remove(product);  
            _context.SaveChanges();  

            return true;  
        }


        public async Task<bool> UpdateMedicineAsync(int id, CreateMedicineViewModel model)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null)
            {
                return false;
            }

            // Update the medicine's properties
            medicine.Name = model.Name;
            medicine.UnitPrice = model.Price;
            medicine.ExpiryDate = model.ExpireDate;
            medicine.StockQuantity = model.Stock;
            medicine.CategoryId = model.CategoryId;

            _context.Medicines.Update(medicine);
            await _context.SaveChangesAsync();
            return true;
        }



        public async Task<IEnumerable<Medicine>> GetLowStockMedicines(int threshold)
        {
            return await _context.Medicines
                                 .Where(m => m.StockQuantity < threshold)
                                 .ToListAsync();
        }

    }
}
