using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{

    private readonly UserService UserService;

    public UserController(UserService userService)
    {
        this.UserService = userService;
    }
    // För att skapa en användare skriv in eran localhost-adress och lägg till /register ex. http://localhost:5206/register
    // skapa ett json-objekt med email och password. exempel nedan
    /* {
  "email": "ingen@ingen.se",
  "password":"Hejsan1!"
} */

// För att logga in lägg till login, istället för register ex. http://localhost:5206/login

[HttpDelete("remove/{id}")]
public async Task<IActionResult> RemoveUser(Guid id)
{
    try
    {
        User user = await UserService.RemoveUser(id);
        //UserDtoMessage output = new(user);
        return Ok(user);
    }
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }
} 
}