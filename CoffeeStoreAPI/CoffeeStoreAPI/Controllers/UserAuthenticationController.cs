using CoffeeStoreAPI.Iterfaces;
using CoffeeStoreAPI.Models;
using CoffeeStoreAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserAuthenticationController(IUserService userService) 
        {
            _userService=userService;
        }

        [HttpPost("Register")]
        [ProducesResponseType(typeof(UserDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDetails>> RegisterManager(RegisterUserDTO registerUserDTO)
        {
            try
            {
                var res = await _userService.RegisterUser(registerUserDTO,2);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(400,ex.Message));
            }
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(TokenDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TokenDTO>> LoginUser(LoginDTO loginDTO)
        {
            try
            {
                var res = await _userService.LoginUser(loginDTO);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(401,ex.Message));
            }
        }
    }
}
