using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using GratefulStores.Dtos;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GratefulStores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
    private readonly IRepository<Product> _prodRepo;
    private readonly IRepository<ProductTypes> _prodTypesRepo;
    private readonly IRepository<ProductBrands> _prodBrandsRepo;
    private readonly IMapper _mapper;

        public ProductsController(IRepository<Product> prodRepo, IRepository<ProductTypes> prodTypesRepo, IRepository<ProductBrands> prodBrandsRepo, IMapper mapper)
        {
            _prodRepo = prodRepo;
            _prodTypesRepo = prodTypesRepo;
            _prodBrandsRepo = prodBrandsRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = await _prodRepo.ListAsync(spec);

            //var productsToReturn = new List<ProductToReturnDto>();
            //foreach (var prod in products)
            //{
            //    var prods = new ProductToReturnDto()
            //    {
            //        Id = prod.Id,
            //        Name = prod.Name,
            //        Description = prod.Description,
            //        PictureUrl = prod.PictureUrl,
            //        Price = prod.Price,
            //        ProductBrand = prod.ProductBrand.Name,
            //        ProductType = prod.ProductType.Name
            //    };
            //    productsToReturn.Add(prods);
            //}
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _prodRepo.GetEntityWithSpec(spec);

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetProductBrands()
        {
            var brands = await _prodBrandsRepo.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetProductTypes()
        {
            var types = await _prodTypesRepo.GetAllAsync();
            return Ok(types);
        }
    }
}
