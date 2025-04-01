public class User
{
    public Guid Id { get; set; }
    public string Mail { get; set; } = "";
    public string Password { get; set; } = "";
    public List<Product> Products { get; set; } = new();
    public User(string mail, string password)
    {
        this.Mail = mail;
        this.Password = password;
        this.Id = Guid.NewGuid();
    }
    public User() { }

}
//------------------------------------------------------------
public class UserDtoRequest
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
}