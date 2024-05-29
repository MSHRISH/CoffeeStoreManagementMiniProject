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
    public class ItemServicesController : ControllerBase
    {
        private readonly IItemServices _itemServices;

        public ItemServicesController(IItemServices itemServices) 
        { 
            _itemServices=itemServices;
        }

        [HttpGet("GetAllItems")]
        [ProducesResponseType(typeof(List<ItemDetailsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ItemDetailsDTO>>> GetAllItems()
        {
            try
            {
                var res = await _itemServices.GetAllItems();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(404, ex.Message));
            }
        }

        [HttpGet("GetAllAvailableItems")]
        [ProducesResponseType(typeof(List<ItemDetailsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ItemDetailsDTO>>> GetAllAvailableItems()
        {
            try
            {
                var res = await _itemServices.GetAllItems(isAvailable: true);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(404, ex.Message));
            }
        }
        [HttpGet("GetAllUnavailableItems")]
        [ProducesResponseType(typeof(List<ItemDetailsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ItemDetailsDTO>>> GetAllUnavailableItems()
        {
            try
            {
                var res = await _itemServices.GetAllItems(isAvailable: false);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(404, ex.Message));
            }
        }

        [HttpPost("ChangeAvailabilityofItem")]
        [Authorize(Policy = "RequireStoreEmployee")]
        [ProducesResponseType(typeof(ItemDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemDetailsDTO>> ChangeAvailabilityofItem(int itemid)
        {
            try
            {
                var res = await _itemServices.ChangeAvailabilityOfItem(itemid);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(404, ex.Message));
            }
        }

        [HttpPost("AddAnItem")]
        [Authorize(Policy = "RequireAdminOrManagerRole")]
        [ProducesResponseType(typeof(ItemDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ItemDetailsDTO>> AddAnItem(AddItemDTO itemDTO)
        {
            try
            {
                var res = await _itemServices.AddItem(itemDTO);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(500, ex.Message));
            }
        }

        [HttpGet("GetItemById/{itemid}")]
        [ProducesResponseType(typeof(ItemDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemDetailsDTO>> GetItemById(int itemid)
        {
            try
            {
                var res = await _itemServices.GetItemById(itemid);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(404, ex.Message));
            }
        }

        [HttpGet("GetItemTypeById/{itemTypeId}")]
        [ProducesResponseType(typeof(ItemTypeDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemTypeDetailsDTO>> GetItemTypeById(int itemTypeId)
        {
            try
            {
                var res = await _itemServices.GetItemTypeById(itemTypeId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(404, ex.Message));
            }
        }

        [HttpPost("AddAnItemType")]
        [Authorize(Policy = "RequireAdminOrManagerRole")]
        [ProducesResponseType(typeof(ItemTypeDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ItemTypeDetailsDTO>> AddAnItemType(AddItemTypeDTO addItemTypeDTO)
        {
            try
            {
                var res = await _itemServices.AddItemType(addItemTypeDTO);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(500, ex.Message));
            }
        }

        [HttpGet("GetAllItemTypes")]
        [ProducesResponseType(typeof(List<ItemTypeDetailsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ItemTypeDetailsDTO>>> GetAllItemTypes()
        {
            try
            {
                var res = await _itemServices.GetAllItemTypes();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(404, ex.Message));
            }
        }


        [HttpGet("GetAllItemsByTypeId/{itemTypeId}")]
        [ProducesResponseType(typeof(ItemTypeDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemTypeDetailsDTO>> GetItemsByTypeId(int itemTypeId)
        {
            try
            {
                var res = await _itemServices.GetItemsByTypeId(itemTypeId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(404, ex.Message));
            }
        }
    }
}
