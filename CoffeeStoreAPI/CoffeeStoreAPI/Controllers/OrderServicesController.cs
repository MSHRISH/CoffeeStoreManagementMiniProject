using CoffeeStoreAPI.Iterfaces;
using CoffeeStoreAPI.Models;
using CoffeeStoreAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CoffeeStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderServicesController : ControllerBase
    {
        private readonly IOrderServices _orderServices;

        public OrderServicesController(IOrderServices orderServices) 
        {
            _orderServices=orderServices;
        }

        [HttpPost("OpenAnOrder")]
        [Authorize(Policy = "RequireCustomerRole")]
        [ProducesResponseType(typeof(OrderDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDetailsDTO>> OpenAnOrder()
        {
            try
            {
                int.TryParse(User.FindFirst(ClaimTypes.Name)?.Value, out int parsedUserId);
                var res = await _orderServices.OpenAnOrder(parsedUserId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(404, ex.Message));
            }
        }
    }
}
