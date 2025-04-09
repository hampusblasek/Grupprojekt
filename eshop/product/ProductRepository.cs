using Microsoft.EntityFrameworkCore;

public interface IProductRepository
{
    Task AddProduct(Product product, User user); // Add new product
    Task<List<ProductResponseDto>> GetAllProducts(); // Returns a list with all products
    Task<Product?> FindProduct(string title); // Returns a product with matching titles
    Task<Product?> FindProductById(Guid id); // Returns a product with matching id
    Task<User?> FindById(string id); // Returns a user with matching id
    Task DeleteProduct(Guid id); // Delete a product - a user can only delete its own products
    Task<List<Product>> GetProductsByUserId(string userId); // Returns a list of users products
    Task UpdateProductStockStatus(Guid productId, bool inStock); // update stock-status on a specific product
}

public class ProductRepository : IProductRepository
{ 

    private readonly AppContext Context;

    public ProductRepository(AppContext context)
    {
        this.Context = context;
    }

    public async Task AddProduct(Product product, User user)
    {
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

    public async Task DeleteProduct(Guid productId)
    {
        await Context.Product.Where(p => p.Id.Equals(productId)).ExecuteDeleteAsync();
        await Context.SaveChangesAsync();
    }

    public async Task<User?> FindById(string id)
    { // Change to string, as Identity Core uses string for Id

        return await Context.Users.FindAsync(id);
    }

    public async Task<Product?> FindProductById(Guid id)
    { // Change to string, as Identity Core uses string for Id

        return await Context.Product.FindAsync(id);
    }


    //  Get products by User ID ------Rami
    public async Task<List<Product>> GetProductsByUserId(string userId)
    {
        return await Context.Product
            .Where(p => p.User.Id == userId) // Get products where the User id matches
            .ToListAsync();
    }


    // Update the InStock status of a product
    public async Task UpdateProductStockStatus(Guid productId, bool inStock)
    {
        var product = await Context.Product.FindAsync(productId);

        if (product != null)
        {
            product.InStock = inStock;
            await Context.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentException("Product not found.");
        }
    }

    public async Task<Product?> FindProduct(string title)
{
    if (string.IsNullOrWhiteSpace(title))
    {
        return null; 
    }
 
    return await Context.Product
        .Include(p => p.User) 
        .FirstOrDefaultAsync(p => p.Title.ToLower().Contains(title.ToLower()));
}

}