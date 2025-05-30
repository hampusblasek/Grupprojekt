using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Grupprojekt;

[ApiController]
[Route("product")]
public class ProductController : ControllerBase
{
    private readonly IProductService ProductService;

    public ProductController(IProductService productService)
    {
        this.ProductService = productService;
    }

    // Add new product
    [Authorize] // Only authenticated users can add products
    [HttpPost("new")]
    public async Task<IActionResult> NewProduct([FromBody] ProductCreateDto dto)
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

            Product product = await ProductService.RegisterProduct(userId, dto.Title, dto.Description, dto.Price);
            ProductResponseDto output = new(product);
            return Ok(output);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpGet("sort/price")]
    public async Task<IActionResult> SortProductsByPrice()
    {
        try
        {
            List<ProductResponseDto> product = await ProductService.SortProductsByPrice();

            return Ok(product);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetProducts()
    {
        try
        {
            List<ProductResponseDto> product = await ProductService.GetProducts(Guid.Empty);
            return Ok(product);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("user")]
    [Authorize]  // Makes sure the user is authenticated
    public async Task<IActionResult> GetMyProducts() // Show all products from a specific user (based on the authenticated user)
    {
        try
        {

            // Get userId from the token
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException("User ID is missing.");

            // Get the user products 
            var products = await ProductService.GetMyProducts(userId);
            return Ok(products);
        }

        // Return 400 Bad Request if user ID is invalid
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }

        // Return 404 Not Found if no products or user not found
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }

        // Catch other unexpected errors
        catch (Exception e)
        {
            return BadRequest("Unexpected error: " + e.Message);
        }
    }

    // Updates product inStock status
    [HttpPut("instock/{productId}")]
    [Authorize]
    public async Task<IActionResult> UpdateInStockStatus(Guid productId, [FromBody] bool inStock)
    {
        try
        {
            // Get the userId from the token (assuming you're using JWT authentication)
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException("User ID is missing.");

            // Update the inStock status
            await ProductService.UpdateProductStockStatus(productId, inStock);
            return Ok("Product stock status was updated successfully.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("search/{title}")]
public async Task<IActionResult> FindProducts(string title)
{
    try
    {
        Product product = await ProductService.FindProduct(title);
        ProductResponseDto output = new(product); 
        return Ok(output);
    }
     // If an error occurs (e.g., invalid input or no product found), return a 400 Bad Request with the error message
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }
}

[HttpDelete("remove/{id}")]
[Authorize]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        try
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId == null) throw new ArgumentNullException("User not found");

            Product product = await ProductService.DeleteProduct(id, userId);
            ProductResponseDto output = new(product);
            return Ok(output);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}