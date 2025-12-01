using Asisya.Application.Interfaces;
using Asisya.Domain.Entities;
using Asisya.Infrastructure.Data;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace Asisya.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AsisyaDbContext _context;

    public ProductRepository(AsisyaDbContext context) => _context = context;

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<(IEnumerable<Product> Products, int Total)> GetPagedAsync(int page, int size, string? search = null)
    {
        var query = _context.Products.Include(p => p.Category).AsQueryable();
        
        if (!string.IsNullOrEmpty(search))
            query = query.Where(p => p.Name.Contains(search) || p.Sku.Contains(search));

        var total = await query.CountAsync();
        var products = await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();

        return (products, total);
    }

    public async Task<Product> CreateAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task BulkInsertAsync(IEnumerable<Product> products)
    {
        await _context.BulkInsertAsync(products);
    }
}

public class CategoryRepository : ICategoryRepository
{
    private readonly AsisyaDbContext _context;

    public CategoryRepository(AsisyaDbContext context) => _context = context;

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category> CreateAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }
}