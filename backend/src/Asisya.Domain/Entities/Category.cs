using System;
using System.Collections.Generic;
namespace Asisya.Domain.Entities {
 public class Category {
  public Guid Id{get;set;}=Guid.NewGuid();
  public string Name{get;set;}="";
  public string? ImageUrl{get;set;}
  public ICollection<Product> Products{get;set;}=new List<Product>();
 }}