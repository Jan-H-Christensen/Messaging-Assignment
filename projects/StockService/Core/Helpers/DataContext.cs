using Microsoft.EntityFrameworkCore;
using StockService.Core.Entities;

namespace StockService.Core.Helpers;

public class DataContext : DbContext {

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // in memory database used for simplicity, change to a real db for production applications
        options.UseInMemoryDatabase("TestDb");
    }
    
    public DbSet<Product> Products { get; set; }
}