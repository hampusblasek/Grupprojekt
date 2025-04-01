public interface IProductService{
Task<Product> RegisterProduct(Guid id, string title, string description, double price);
Task<List<ProductDto>> GetProducts(Guid id);
Task<IEnumerable<ProductDto>> GetMyProducts(Guid id);
Task<Product> FindProduct(string title);
Task<Product> DeleteProduct(Guid userId, Guid productId);
}

/* public class ProductService : IProductService{

    private readonly ProductRepository ProductRepository;

    public ProductService(ProductRepository productRepository)
    {
        this.ProductRepository = productRepository;
    }

    
}  */