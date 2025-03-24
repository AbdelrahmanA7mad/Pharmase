using Microsoft.EntityFrameworkCore;
using Pharmase.Models;

namespace Pharmase.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }


    }
}
