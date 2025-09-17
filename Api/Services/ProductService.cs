using Api.Contracts;
using Api.Data;
using Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;
public class ProductService : IProductService {
    private readonly AppDbContext _db;
    public ProductService(AppDbContext db) {
        _db = db;
    }

    public async Task<(IReadOnlyList<Product>,int)> GetAsync(string? q,int page,int size) {
        var query = _db.Products.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(q)) query = query.Where(p => p.Name.Contains(q));
        var total = await query.CountAsync();
        var items = await query.OrderByDescending(p => p.Id)
                               .Skip((page-1)*size).Take(size).ToListAsync();
        return (items,total);
    }

    public Task<Product?> GetByIdAsync(int id) => _db.Products.FindAsync(id).AsTask();

    public async Task<Product> CreateAsync(Product p) 
    { 
        _db.Add(p); 
        await _db.SaveChangesAsync(); 
        return p; 
    }

    public async Task UpdateAsync(int id, Product p) {
        var e = await _db.Products.FirstOrDefaultAsync(x => x.Id==id) ?? throw new KeyNotFoundException();
        e.Name=p.Name;
        e.Price=p.Price; 
        e.Stock=p.Stock;
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id) {
        var e = await _db.Products.FindAsync(id) ?? throw new KeyNotFoundException();
        _db.Remove(e); 
        await _db.SaveChangesAsync();
    }
}
