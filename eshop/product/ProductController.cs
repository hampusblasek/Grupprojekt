using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("product")]
public class ProductController : ControllerBase
{
    /* private readonly ProductService ProductService;

    public ProductController(ProductService productService)
    {
        this.ProductService = productService;
    }
    [HttpPost("new/{id}")]
    public async  Task<IActionResult> NewUser(Guid id, [FromBody] ProductDto dto)
    {
        try
        {
            Product product = await ProductService.RegisterProduct(id, dto.Title, dto.Description,dto.Price);
            ProductDto output = new(product);
            return Ok(output);
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
            List<ProductDto> product = await ProductService.GetProducts();

            return Ok(product);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

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
    } */
}