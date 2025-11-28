using System;
namespace Asisya.Domain.Entities {
 public class Product {
  public Guid Id {get;set;}=Guid.NewGuid();
  public string Name {get;set;}=""
; public string Sku {get;set;}="";
  public decimal Price{get;set;}
  public Guid CategoryId{get;set;}
  public Category? Category{get;set;}
  public DateTime CreatedAt{get;set;}=DateTime.UtcNow;
 }}