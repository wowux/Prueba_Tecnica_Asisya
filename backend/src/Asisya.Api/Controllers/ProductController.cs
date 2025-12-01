using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asisya.Application.DTOs;
using Asisya.Application.Interfaces;
using Asisya.Domain.Entities;

namespace Asisya.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromQuery] int count = 1)
    {
        var categories = await _categoryRepository.GetAllAsync();
        var categoryList = categories.ToList();
        
        if (!categoryList.Any())
            return BadRequest("No categories available. Create categories first.");

        var products = new List<Product>();
        var random = new Random();

        for (int i = 0; i < count; i++)
        {
            var category = categoryList[random.Next(categoryList.Count)];
            products.Add(new Product
            {
                Name = $"Product {Guid.NewGuid().ToString()[..8]}",
                Sku = $"SKU-{random.Next(100000, 999999)}",
                Price = Math.Round((decimal)(random.NextDouble() * 1000 + 10), 2),
                CategoryId = category.Id
            });
        }

        if (count > 1000)
            await _productRepository.BulkInsertAsync(products);
        else
            foreach (var product in products)
                await _productRepository.CreateAsync(product);

        return Ok(new { Message = $"{count} products created successfully" });
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int size = 20, [FromQuery] string? search = null)
    {
        var (products, total) = await _productRepository.GetPagedAsync(page, size, search);
        
        var result = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Sku = p.Sku,
            Price = p.Price,
            CategoryId = p.CategoryId,
            CategoryName = p.Category?.Name,
            CategoryImageUrl = p.Category?.ImageUrl,
            CreatedAt = p.CreatedAt
        });

        return Ok(new { Products = result, Total = total, Page = page, Size = size });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return NotFound();

        var result = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Sku = product.Sku,
            Price = product.Price,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name,
            CategoryImageUrl = product.Category?.ImageUrl,
            CreatedAt = product.CreatedAt
        };

        return Ok(result);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateProductDto dto)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return NotFound();

        product.Name = dto.Name;
        product.Sku = dto.Sku;
        product.Price = dto.Price;
        product.CategoryId = dto.CategoryId;

        await _productRepository.UpdateAsync(product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _productRepository.DeleteAsync(id);
        return NoContent();
    }
}