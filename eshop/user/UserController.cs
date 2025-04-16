using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Grupprojekt;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{

    private readonly IUserService UserService;

    public UserController(IUserService userService)
    {
        this.UserService = userService;
    }

[HttpDelete("remove")]
    [Authorize("remove_user")]
    public async  Task<IActionResult> RemoveUser()
    {
        try
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("No identified user");
        }
            
            User removeUser = await UserService.RemoveUser(id);
            return Ok(removeUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}