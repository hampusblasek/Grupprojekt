using Microsoft.EntityFrameworkCore;

public interface IProductRepository{
Task AddProduct(Product product, User user); // Add new product
Task<List<ProductResponseDto>> GetAllProducts(); // Returns a list with all products
Task<Product> FindProduct(string title); // Returns a product with matching titles
Task<User?> FindById(string id);
//Task<User?> FindUser(Guid id); // Returns a user with matching id
Task DeleteProduct(Guid id); // Delete a product - a user can only delete its own products
}

public class ProductRepository  { // : IProductRepository

    private readonly AppContext Context;

    public ProductRepository(AppContext context){
        this.Context = context;
    }

    public async Task AddProduct(Product product, User user){
        Context.Product.Add(product);
        await Context.SaveChangesAsync();
    }

     public async Task<List<ProductResponseDto>> GetAllProducts()
    {
        return await Context.Product
            .Include(p => p.User)
            .Select(p => new ProductResponseDto(p)) 
            .ToListAsync();
    }

    public async Task DeleteProduct(Guid id){

        await Context.Product.Where(pr => pr.Id.Equals(id)).ExecuteDeleteAsync();
        await Context.SaveChangesAsync();
    } 

    public async Task<User?> FindById(string id){ // Change to string, as Identity Core uses string for Id

        return await Context.Users.FindAsync(id);
    }
    
}