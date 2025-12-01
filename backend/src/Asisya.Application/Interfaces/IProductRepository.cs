using Asisya.Domain.Entities;

namespace Asisya.Application.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<(IEnumerable<Product> Products, int Total)> GetPagedAsync(int page, int size, string? search = null);
    Task<Product> CreateAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task DeleteAsync(Guid id);
    Task BulkInsertAsync(IEnumerable<Product> products);
}

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid id);
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category> CreateAsync(Category category);
}