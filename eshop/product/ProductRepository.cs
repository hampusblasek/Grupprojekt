public interface IProductRepository{
Task AddProduct(Product product, User user);
Task<List<ProductDto>> GetAllProducts();
Task<Product> FindProduct(string title);
Task<User?> FindUser(Guid id);
Task DeleteProduct(Guid id);
}

/* public class ProductRepository : IProductRepository{

    private readonly AppContext Context;

    public ProductRepository(AppContext context){
        this.Context = context;
    }

    public async Task DeleteProduct(Guid id){

        await Context.Product.Where(pr => pr.Id.Equals(id)).ExecuteDeleteAsync();
        await Context.SaveChangesAsync();
    } 
    
}  */