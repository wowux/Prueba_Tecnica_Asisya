using Asisya.Domain.Entities;
using Asisya.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Asisya.Infrastructure.Repositories {
 public class ProductRepository {
  private readonly AsisyaDbContext _db;
  public ProductRepository(AsisyaDbContext db)=>_db=db;
 }}