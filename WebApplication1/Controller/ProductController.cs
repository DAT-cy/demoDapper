using Microsoft.AspNetCore.Mvc;
using ProductWebApi.ProductService;
using Share;

namespace WebApplication1.Controller;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddProduct( Product product)
    {
        await _productService.AddProduct(product);
        return Created();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteProduct(id);
        return NoContent();
    }
    
}