using Microsoft.EntityFrameworkCore;
using Asisya.Domain.Entities;
using Asisya.Infrastructure.Data;
using Asisya.Infrastructure.Repositories;
using Xunit;

namespace Asisya.Tests;

public class ProductRepositoryTests
{
    private AsisyaDbContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AsisyaDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AsisyaDbContext(options);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsProduct_WhenExists()
    {
        using var context = GetInMemoryContext();
        var repository = new ProductRepository(context);
        
        var category = new Category { Name = "Test Category" };
        var product = new Product { Name = "Test Product", Sku = "TEST-001", Price = 100, Category = category };
        
        context.Categories.Add(category);
        context.Products.Add(product);
        await context.SaveChangesAsync();

        var result = await repository.GetByIdAsync(product.Id);

        Assert.NotNull(result);
        Assert.Equal("Test Product", result.Name);
    }

    [Fact]
    public async Task GetPagedAsync_ReturnsCorrectPage()
    {
        using var context = GetInMemoryContext();
        var repository = new ProductRepository(context);
        
        var category = new Category { Name = "Test Category" };
        context.Categories.Add(category);
        
        for (int i = 1; i <= 25; i++)
        {
            context.Products.Add(new Product 
            { 
                Name = $"Product {i}", 
                Sku = $"SKU-{i:000}", 
                Price = i * 10, 
                CategoryId = category.Id 
            });
        }
        await context.SaveChangesAsync();

        var (products, total) = await repository.GetPagedAsync(2, 10);

        Assert.Equal(25, total);
        Assert.Equal(10, products.Count());
    }
}