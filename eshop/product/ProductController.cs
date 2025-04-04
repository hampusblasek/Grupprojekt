using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("product")]
public class ProductController : ControllerBase
{
    private readonly ProductService ProductService;

    public ProductController(ProductService productService)
    {
        this.ProductService = productService;
    }

    // Add new product
    [Authorize] // Only authenticated users can add products
    [HttpPost("new")]
    public async  Task<IActionResult> NewProduct([FromBody] ProductResponseDto dto)
    {
        // This will validate the validation attributes in the ProductResponseDto class
        if (!ModelState.IsValid)
        {
            // If not valid, ModelState will contain the errors
            return BadRequest(ModelState);
        }

        try
        {
            // string userId = dto.UserId; // Old way of getting the userId from the DTO
            
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // New way of getting the userId from the token
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("UserId is missing in the token.");
            }

            Product product = await ProductService.RegisterProduct(userId, dto.Title, dto.Description,dto.Price);
            ProductResponseDto output = new(product);
            return Ok(output);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    /*
    // show all products
    [HttpGet("all")]
    public async Task<IActionResult> GetProducts()
    {
        try
        {
            List<ProductDto> product = await ProductService.GetProducts();

            return Ok(product);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // show all products from a specific user
    [HttpGet("my/{id}")]
    public async Task<IActionResult> GetMyProducts(Guid id)
    {
        try
        {
            IEnumerable<ProductDto> product = await ProductService.GetMyProducts(id);
            
            return Ok(product);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    // search for a specific product
    [HttpGet("search/{title}")]
    public async Task<IActionResult> FindProducts(string title)
    {
        try
        {
            Product product = await ProductService.FindProduct(title);
            
            return Ok(product);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    //Delete a product
    [HttpDelete("remove")]
    public async  Task<IActionResult> RemoveProduct([FromQuery] Guid userId, Guid productId )
    {
        try
        {
            Product product = await ProductService.RemoveProduct(userId, productId);
            ProductDto output = new(product);
            return Ok(output);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        } 
    }   */
}