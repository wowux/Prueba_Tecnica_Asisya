using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asisya.Application.DTOs;
using Asisya.Application.Interfaces;
using Asisya.Domain.Entities;

namespace Asisya.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            ImageUrl = dto.ImageUrl
        };

        var created = await _categoryRepository.CreateAsync(category);
        
        var result = new CategoryDto
        {
            Id = created.Id,
            Name = created.Name,
            ImageUrl = created.ImageUrl
        };

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, result);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var result = categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name,
            ImageUrl = c.ImageUrl
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null) return NotFound();

        var result = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            ImageUrl = category.ImageUrl
        };

        return Ok(result);
    }
}