using OrderService.Core.Entities;

namespace OrderService.Core.Helpers;

using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext {

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseInMemoryDatabase("TestDb");
    }
    
    public DbSet<Order> Orders { get; set; }
}