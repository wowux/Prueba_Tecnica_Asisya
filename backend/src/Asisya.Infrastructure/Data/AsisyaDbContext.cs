using Microsoft.EntityFrameworkCore;
using Asisya.Domain.Entities;
namespace Asisya.Infrastructure.Data {
 public class AsisyaDbContext:DbContext{
  public AsisyaDbContext(DbContextOptions<AsisyaDbContext>o):base(o){}
  public DbSet<Product> Products{get;set;}
  public DbSet<Category> Categories{get;set;}
 }}