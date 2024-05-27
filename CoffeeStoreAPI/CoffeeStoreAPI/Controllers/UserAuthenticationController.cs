using CoffeeStoreAPI.Iterfaces;
using CoffeeStoreAPI.Models;
using CoffeeStoreAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("RegisterManager")]
        [Authorize(Policy = "RequireAdminRole")]
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
        [HttpPost("RegisterBarista")]
        [Authorize(Policy = "RequireAdminOrManagerRole")]
        [ProducesResponseType(typeof(UserDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDetails>> RegisterBarista(RegisterUserDTO registerUserDTO)
        {
            try
            {
                var res = await _userService.RegisterUser(registerUserDTO, 3);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(400, ex.Message));
            }
        }

        [HttpPost("RegisterCustomer")]
        [ProducesResponseType(typeof(UserDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDetails>> RegisterCustomer(RegisterUserDTO registerUserDTO)
        {
            try
            {
                var res = await _userService.RegisterUser(registerUserDTO, 4);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(400, ex.Message));
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
        [HttpGet("GetBaristaById/{id}")]
        [Authorize(Policy = "RequireAdminOrManagerRole")]
        [ProducesResponseType(typeof(UserDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDetails>> GetBaristaById(int id)
        {
            try
            {
                //var res = await _userService.GetBaristaById(id);
                var res = await _userService.GetUserById(id, 3);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(404, ex.Message));
            }
        }
        [HttpGet("GetManagerById/{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        [ProducesResponseType(typeof(UserDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDetails>> GetManagerById(int id)
        {
            try
            {
                //var res = await _userService.GetManagerById(id);
                var res=await _userService.GetUserById(id,2);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(404, ex.Message));
            }
        }

        [HttpGet("GetCustomerById/{id}")]
        [Authorize(Policy = "RequireAdminOrManagerRole")]
        [ProducesResponseType(typeof(UserDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDetails>> GetCustomerById(int id)
        {
            try
            {
                //var res = await _userService.GetCustomerById(id);
                var res = await _userService.GetUserById(id, 4);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(404, ex.Message));
            }
        }

        [HttpGet("GetAllManagers")]
        [Authorize(Policy ="RequireAdminRole")]
        [ProducesResponseType(typeof(UserDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<UserDetails>>> GetAllManagers()
        {
            try
            {
                //var res = await _userService.GetAllManagers();
                var res=await _userService.GetAllUsersByRole(2);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(404, ex.Message));
            }
        }

        [HttpGet("GetAllBaristas")]
        [Authorize(Policy = "RequireAdminOrManagerRole")]
        [ProducesResponseType(typeof(UserDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<UserDetails>>> GetAllBaristas()
        {
            try
            {
                //var res = await _userService.GetAllBaristas();
                var res = await _userService.GetAllUsersByRole(3);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(404, ex.Message));
            }
        }

        [HttpGet("GetAllCustomers")]
        [Authorize(Policy = "RequireAdminOrManagerRole")]
        [ProducesResponseType(typeof(UserDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<UserDetails>>> GetAllCustomers()
        {
            try
            {
                //var res = await _userService.GetAllCustomers();
                var res = await _userService.GetAllUsersByRole(4);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(404, ex.Message));
            }
        }
    }
}
