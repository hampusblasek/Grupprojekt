using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class User : IdentityUser
{
    public List<Product> Products { get; set; } = new();
    public User() { }

}
