using System.Text.RegularExpressions; // to use Regex

public interface IProductService
{
    Task<Product> RegisterProduct(string id, string title, string description, bool inStock, double price); // Add new product
    Task<List<ProductResponseDto>> GetProducts(Guid id); // Returns a list with all products
    Task<List<ProductResponseDto>> SortProductsByPrice();
    Task<IEnumerable<ProductResponseDto>> GetMyProducts(Guid id); // Returns a list with a specific users products
    Task<Product> FindProduct(string title); // returns a product with matching titles
    Task<Product> DeleteProduct(Guid userId, Guid productId); // Delete a product - a user can only delete its own products
}

public class ProductService
{ // : IProductService

    private readonly ProductRepository ProductRepository;

    public ProductService(ProductRepository productRepository)
    {
        this.ProductRepository = productRepository;
    }

    public async Task<Product> RegisterProduct(string userId, string title, string description, bool inStock, double price)
    {
        User? user = await ProductRepository.FindById(userId);
        if (user == null)
        {
            throw new ArgumentException("No identified user");
        }
        // Validate product title 
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be empty");
        }

        if (title.Length < 3 || title.Length > 100)
        {
            throw new ArgumentException("Title must be between 3 and 100 characters");
        }

        Regex validInput = new Regex(@"^[a-zA-Z0-9.,\s]+$");
        if (!validInput.IsMatch(title))
        {
            throw new ArgumentException("Title can only contain letters, numbers, spaces, commas, and periods");
        }

        // Validate product description
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description cannot be empty");
        }
        if (description.Length < 5 || description.Length > 1000)
        {
            throw new ArgumentException("Description must be between 5 and 1000 characters");
        }

        if (!validInput.IsMatch(description))
        {
            throw new ArgumentException("Description can only contain letters, numbers, spaces, commas, and periods");
        }

        // Validate product price
        if (price <= 0 || price > 1000000)
        {
            throw new ArgumentException("Price must be greater than 0 and less than 1,000,000");
        }

        Product product = new Product(title, description, price, inStock, user);
        await ProductRepository.AddProduct(product, user);
        return product;
    }


    public async Task<List<ProductResponseDto>> SortProductsByPrice()
    {
        List<ProductResponseDto> productList = await ProductRepository.GetAllProducts(); // Will get all products from getAllProducts

        var sortedList = productList.OrderBy(product => product.Price).ToList();

        return sortedList;
    }


    // Get all products for a specific user ---- Rami
    public async Task<IEnumerable<ProductResponseDto>> GetMyProducts(string userId)
    {
        // Check if userId is null or empty
        if (string.IsNullOrEmpty(userId))
        {
            throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));
        }

        var products = await ProductRepository.GetProductsByUserId(userId);

        // Check if products are found
        if (products == null || !products.Any())
        {
            throw new KeyNotFoundException($"No products found for user with this ID {userId}.");
        }

        // Return the products as ProductResponseDto
        return products.Select(product => new ProductResponseDto(product)).ToList();
    }


    // Update the InStock status of a product
    public async Task UpdateProductStockStatus(Guid productId, bool inStock)
    {
        await ProductRepository.UpdateProductStockStatus(productId, inStock);
    }

}