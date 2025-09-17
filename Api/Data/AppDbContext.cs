using Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;
public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().Property(p => p.RowVersion).IsRowVersion();
        modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(150);
        modelBuilder.Entity<Product>().HasIndex(p => p.Name);
        modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
    }
}