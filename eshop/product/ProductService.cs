public interface IProductService{
Task<Product> RegisterProduct(Guid id, string title, string description, double price); // Add new product
Task<List<ProductDto>> GetProducts(Guid id); // Returns a list with all products
Task<IEnumerable<ProductDto>> GetMyProducts(Guid id); // Returns a list with a specific users products
Task<Product> FindProduct(string title); // returns a product with matching titles
Task<Product> DeleteProduct(Guid userId, Guid productId); // Delete a product - a user can only delete its own products
}

public class ProductService {

    private readonly ProductRepository ProductRepository;

    public ProductService(ProductRepository productRepository)
    {
        this.ProductRepository = productRepository;
    }

    
} 