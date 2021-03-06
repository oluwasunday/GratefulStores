using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
  public class ProductRepository : IRepository<Product>, IProductRepository
  {
    private readonly StoreContext _context;

    public ProductRepository(StoreContext context)
    {
      _context = context;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
      return await _context.Products
                .Include(x => x.ProductType)
                .Include(x => x.ProductBrand)
                .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyList<ProductBrands>> GetProductBrandsAsync()
    {
        return await _context.ProductsBrands.ToListAsync();
    }

    public async Task<IReadOnlyList<ProductTypes>> GetProductTypesAsync()
    {
        return await _context.ProductsTypes.ToListAsync();
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
      return await _context.Products
                .Include(x => x.ProductType)
                .Include(x => x.ProductBrand)
                .ToListAsync();
    }

        public Task<Product> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetEntityWithSpec(ISpecifications<Product> spec)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Product>> ListAsync(ISpecifications<Product> spec)
        {
            throw new NotImplementedException();
        }
    }
}