public class Product
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public double Price { get; set; }
    public User User { get; set; }

    public Product(string title, string description, double price, User user)
    {
        this.Title = title;
        this.Description = description;
        this.Price = price;
        this.User = user;
        this.Id = Guid.NewGuid();
    }
    public Product() { }
}
//------------------------------------------------------------
public class ProductDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public double Price { get; set; }
    public ProductDto(Product product)
    {
        this.Title = product.Title;
        this.Description = product.Description;
        this.Price = product.Price;
        this.Id = product.Id;
    }
    public ProductDto() { }

}