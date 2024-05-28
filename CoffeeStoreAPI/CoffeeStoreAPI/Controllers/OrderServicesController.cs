﻿using CoffeeStoreAPI.Iterfaces;
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

        [HttpPost("AddToAnOrder")]
        [Authorize(Policy = "RequireCustomerRole")]
        [ProducesResponseType(typeof(OrderDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDetailsDTO>> AddToAnOrder(AddOrderItemDTO orderItemDTO)
        {
            try
            {
                int.TryParse(User.FindFirst(ClaimTypes.Name)?.Value, out int parsedUserId);
                var res = await _orderServices.AddToOrder(orderItemDTO, parsedUserId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(404,ex.Message));
            }
        }

        [HttpGet("GetOrderById/{orderid}")]
        [Authorize(Policy = "RequireStoreEmployee")]
        [ProducesResponseType(typeof(OrderDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDetailsDTO>> GetOrderById(int orderid)
        {
            try
            {
                var res = await _orderServices.GetOrderDetails(orderid);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(404, ex.Message));
            }
        }

        [HttpGet("GetMyOrderDetails/{orderid}")]
        [Authorize(Policy = "RequireCustomerRole")]
        [ProducesResponseType(typeof(OrderDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDetailsDTO>> GetMyOrderDetails(int orderid)
        {
            try
            {
                int.TryParse(User.FindFirst(ClaimTypes.Name)?.Value, out int parsedUserId);
                var res = await _orderServices.GetMyOrderDetails(orderid, parsedUserId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(404, ex.Message));
            }
        }

        [HttpDelete("CancelOrderItemByStore")]
        [Authorize(Policy = "RequireAdminOrManagerRole")]
        [ProducesResponseType(typeof(OrderDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDetailsDTO>> CancelOrderItemByStore(CancelOrderItemDTO cancelOrderItem)
        {
            try
            {
                var res = await _orderServices.CancelOrderItemByStore(cancelOrderItem);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(404,ex.Message));
            }
        }

        [HttpDelete("CancelOrderItemByCustomer")]
        [Authorize(Policy = "RequireCustomerRole")]
        [ProducesResponseType(typeof(OrderDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDetailsDTO>> CancelOrderItemByCustomer(CancelOrderItemDTO cancelOrderItem)
        {
            try
            {
                int.TryParse(User.FindFirst(ClaimTypes.Name)?.Value, out int parsedUserId);
                var res = await _orderServices.CancelOrderItemByCustomer(cancelOrderItem,parsedUserId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(404, ex.Message));
            }
        }

    }
}