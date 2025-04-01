using Microsoft.EntityFrameworkCore;

public interface IProductRepository{
Task AddProduct(Product product, User user); // Add new product
Task<List<ProductDto>> GetAllProducts(); // Returns a list with all products
Task<Product> FindProduct(string title); // Returns a product with matching titles
Task<User?> FindUser(Guid id); // Returns a user with matching id
Task DeleteProduct(Guid id); // Delete a product - a user can only delete its own products
}

public class ProductRepository : IProductRepository{

    private readonly AppContext Context;

    public ProductRepository(AppContext context){
        this.Context = context;
    }

    public async Task DeleteProduct(Guid id){

        await Context.Product.Where(pr => pr.Id.Equals(id)).ExecuteDeleteAsync();
        await Context.SaveChangesAsync();
    } 

    public async Task<User?> FindById(Guid id){

        return await Context.User.FindAsync(id);
    }
    
}