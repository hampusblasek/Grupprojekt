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
    [HttpPost("new")]
    public async  Task<IActionResult> NewUser([FromBody] UserDtoRequest dto)
    {
        try
        {
            User user = await UserService.RegisterUser(dto.Mail, dto.Password);
            UserDtoMessage output = new(user);
            return Ok(output);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("Login")]
    public async  Task<IActionResult> Login([FromBody] UserDtoRequest dto)
    {
        try
        {
            User user = await UserService.Login(dto.Mail, dto.Password);
            UserDtoMessage output = new(user);
            return Ok(output);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("remove/{id}")]
    public async  Task<IActionResult> RemoveUser(Guid id)
    {
        try
        {
            User user = await UserService.RemoveUser(id);
            UserDtoMessage output = new(user);
            return Ok(output);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    } 
}