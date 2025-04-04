using System.ComponentModel.DataAnnotations;

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
public class ProductResponseDto
{
    // No need to validate the Id, since it is generated by the server
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Title cannot be less than 3 characters or more than 100 characters")]
    [RegularExpression(@"^[a-zA-Z0-9.,\s]+$", ErrorMessage = "Title can only contain letters, numbers, spaces, commas, and periods")]
    public string Title { get; set; } = "";

    [Required(ErrorMessage = "Description is required")]
    [StringLength(1000, MinimumLength = 5, ErrorMessage = "Description cannot be less than 5 characters or more than 1000 characters")]
    [RegularExpression(@"^[a-zA-Z0-9.,\s]+$", ErrorMessage = "Description can only contain letters, numbers, spaces, commas, and periods")]
    public string Description { get; set; } = "";

    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, 1000000, ErrorMessage = "Price must be greater than 0")]
    public double Price { get; set; }
    // public string UserId { get; set; } = ""; // Identity Core uses string for Id // Don't need this since we are using the UserId from the token

    public ProductResponseDto(Product product)
    {
        this.Title = product.Title;
        this.Description = product.Description;
        this.Price = product.Price;
        this.Id = product.Id;
        // this.UserId = product.User?.Id ?? ""; // Don't need this since we are using the UserId from the token
    }
    
    public ProductResponseDto() { }
}