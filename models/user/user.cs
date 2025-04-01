using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class User : IdentityUser
{
    public List<Product> Products { get; set; } = new();
    public User() { }

}
//------------------------------------------------------------
/* public class UserDtoRequest
{
    public Guid Id { get; set; }
    public string Mail { get; set; } = "";
    public string Password { get; set; } = ""; 
    public UserDtoRequest(User user)
    {
        this.Mail = user.Mail;
        this.Id = user.Id;
    }
    public UserDtoRequest() { }
}

public class UserDtoMessage
{
    public Guid Id { get; set; }
    public string Mail { get; set; } = "";
    public UserDtoMessage(User user)
    {
        this.Mail = user.Mail;
        this.Id = user.Id;
    }
    public UserDtoMessage() { }
} */