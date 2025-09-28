using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs;
using Shop.Application.Interfaces;

namespace Shop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductsController(IProductService productService) => _productService = productService;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _productService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        return product is null ? NotFound(new { message = "Product not found." }) : Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        var product = await _productService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
    {
        var product = await _productService.UpdateAsync(id, dto);
        return product is null
            ? NotFound(new { message = "Product not found." })
            : Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _productService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound(new { message = "Product not found." });
    }
}
