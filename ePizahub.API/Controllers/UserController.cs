
using Microsoft.AspNetCore.Mvc;

namespace ePizzahub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly IUserService _userService;

        //public UserController(IUserService userService)
        //{
        //    this._userService = userService;
        //}

        //[HttpPost]
        //[Route("register-user")]
        //public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto request)
        //{
        //    var result = await _userService.RegisterUserAsync(request);
        //    return Ok(result);
        //}


    }
}
