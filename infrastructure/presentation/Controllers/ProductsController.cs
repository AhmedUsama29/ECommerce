using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DataTransferObjects.Products;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")] //BaseUrl/api/Products
    [ApiController]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProducts([FromQuery]ProductQueryParameters productQueryParameters)
        {
            var Products = await _serviceManager.ProductService.GetAllProductsAsync(productQueryParameters);

            return Ok(Products);
        }

        [HttpGet("{id}")]
        public async Task<ProductResponse> GetProductById(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);

            return product;
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandResponse>>> GetAllBrands()
        {
            var brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeResponse>>> GetAllTypes()
        {
            var types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(types);
        }
    }
}
