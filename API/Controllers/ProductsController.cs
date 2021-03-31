using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;
        private readonly StoreContext _storeContext;
        public ProductsController(IProductRepository repo, StoreContext storeContext)
        {
            this._storeContext = storeContext;
            this._repo = repo;

        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProduct()
        {
            var products = await this._repo.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await this._repo.GetProductByIdAsync(id);
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
            var productType = await this._storeContext.ProductTypes.AddAsync(product1);
            await this._storeContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await this._repo.GetProductBrandsAsync());
        }

         [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await this._repo.GetProductTypesAsync());
        }

    }
}