using Microsoft.EntityFrameworkCore;

public interface IProductRepository{
Task AddProduct(Product product, User user); // Add new product
Task<List<ProductDto>> GetAllProducts(); // Returns a list with all products
Task<Product> FindProduct(string title); // Returns a product with matching titles
Task<User?> FindUser(Guid id); // Returns a user with matching id
Task DeleteProduct(Guid id); // Delete a product - a user can only delete its own products
}

public class ProductRepository {

    private readonly AppContext Context;

    public ProductRepository(AppContext context){
        this.Context = context;
    }

    public async Task AddProduct(Product product, User user){
        Context.Product.Add(product);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteProduct(Guid id){

        await Context.Product.Where(pr => pr.Id.Equals(id)).ExecuteDeleteAsync();
        await Context.SaveChangesAsync();
    } 

    public async Task<User?> FindById(string id){

        return await Context.Users.FindAsync(id);
    }
    
}