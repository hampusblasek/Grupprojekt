using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Grupprojekt;

public class User : IdentityUser
{
    public List<Product> Products { get; set; } = new();
    public User() { }

}
