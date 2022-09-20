using Microsoft.AspNetCore.Mvc;
using Gama_API.Data.Dtos;
using Gama_API.Data.Repositorys;
using Gama_API.Model;
using Gama_API.Models.Utils;
using System.ComponentModel;
using System.Collections.Generic;

namespace Gama_API.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : Controller
    {
        readonly ProductRepository _productRepository;
        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpGet(Name = "GetAllProduct")]
        public async Task<IActionResult> Produtos([FromQuery] int n = 10, [FromQuery] string? name = null)
        {
            var products = await _productRepository.GetProductsWhereNameAsync(n, name);
            if(!products.Any())
            {
                return NoContent();
            }

            return Ok(products);
        }


        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<IActionResult> Produto([FromRoute] int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NoContent();
            }

            return Ok(product);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto createProductDto)
        {
            var product = await _productRepository.GetProductByNameAsync(createProductDto.Name);
            if (product != null)
            {
                return BadRequest(new { message = $"Product {product.Name} already exists" });
            }

            product = ConverterDto.Converter<Product, CreateProductDto>(createProductDto);

            await _productRepository.CreateProductAsync(product);

            return CreatedAtAction(nameof(Produto), new { product.Id }, product);
        }


        [HttpPatch("{id}", Name = "UpdateProductById")]
        public async Task<IActionResult> Update([FromRoute] int id, EditProductDto editProductDto)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return BadRequest(new { message = $"There is no product with id {id}" });
            }

            ConverterDto.ConvertInPlace(editProductDto, product, checkNull: true);

            product = await _productRepository.UpdateProductAsync(product);

            return Ok(product);
        }


        [HttpDelete("{id}", Name = "DeleteProductById")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return BadRequest(new {message = $"There is no product with id {id}"});
            }
            
            await _productRepository.DeleteProductAsync(product);

            return Ok();
        }

    }
}
