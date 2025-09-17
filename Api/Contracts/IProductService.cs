using Api.Domain;
namespace Api.Contracts;
public interface IProductService {
    Task<(IReadOnlyList<Product> Items,int Total)> GetAsync(string? q,int page,int size);
    Task<Product?> GetByIdAsync(int id);
    Task<Product> CreateAsync(Product p);
    Task UpdateAsync(int id, Product p);
    Task DeleteAsync(int id);
}