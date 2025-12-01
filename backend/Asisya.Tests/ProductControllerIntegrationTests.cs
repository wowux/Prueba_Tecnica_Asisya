using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using Asisya.Application.DTOs;
using Asisya.Infrastructure.Data;
using Xunit;

namespace Asisya.Tests;

public class ProductControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public ProductControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AsisyaDbContext>));
                if (descriptor != null) services.Remove(descriptor);
                
                services.AddDbContext<AsisyaDbContext>(options =>
                    options.UseInMemoryDatabase("TestDb"));
            });
        });
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetProducts_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/Product?page=1&size=10");
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("Products", content);
    }

    [Fact]
    public async Task Login_WithValidCredentials_ReturnsToken()
    {
        var loginDto = new LoginDto { Username = "admin", Password = "password" };
        var response = await _client.PostAsJsonAsync("/api/Auth/login", loginDto);
        
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("Token", content);
    }
}