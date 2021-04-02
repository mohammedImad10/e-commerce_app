using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> productsRepo;
        private readonly IGenericRepository<ProductBrand> productBrandRepo;
        private readonly IGenericRepository<ProductType> productTypeRepo;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> productsRepo,
        IGenericRepository<ProductBrand> productBrandRepo,
        IGenericRepository<ProductType> productTypeRepo,
        IMapper mapper)
        {
            this.productsRepo = productsRepo;
            this.productBrandRepo = productBrandRepo;
            this.productTypeRepo = productTypeRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductResource>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await this.productsRepo.ListAsync(spec);
            Console.WriteLine(products);
            return Ok(this.mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductResource>>(products));
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResource>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await this.productsRepo.GetEntityWithSpec(spec);

            return this.mapper.Map<Product, ProductResource>(product);
  
        }

        [HttpPost("")]
        public async Task<ActionResult> AddClient()
        {
         
            var product1 = new ProductType{
                Name = "Test",
                // Description= "Lorem ",
                // Price= 200,
                // PictureUrl= "images/products/sb-ang1.png",
                // ProductTypeId= 6,
                // ProductBrandId= 1
            };
            // var productType = await this._storeContext.ProductTypes.AddAsync(product1);
            // await this._storeContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await this.productBrandRepo.ListAllAsync());
        }

         [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await this.productTypeRepo.ListAllAsync());
        }

    }
}