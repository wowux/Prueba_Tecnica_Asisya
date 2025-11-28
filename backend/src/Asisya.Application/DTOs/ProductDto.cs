namespace Asisya.Application.DTOs {
 public class ProductDto {
  public Guid Id{get;set;}
  public string Name{get;set;}
  public string Sku{get;set;}
  public decimal Price{get;set;}
  public Guid CategoryId{get;set;}
  public string CategoryImageUrl{get;set;}
 }}