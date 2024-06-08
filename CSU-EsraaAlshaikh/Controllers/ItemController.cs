namespace CSU_EsraaAlshaikh.Controllers
{
    using CSU_Core.Models;
    using CSU_Core.Service;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]

    public class ItemController : ControllerBase
    {
        private readonly I_ItemService _itemService;

        public ItemController(I_ItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        [Route("list")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllItems()
        {
            try
            {
                var items = await _itemService.GetAllItems();
                if (items.Count() == 0)
                    return Ok(new { Message = "No items found.", Items = new List<Item>() });
                return Ok(new { Message = "Items retrieved successfully.", Items = items });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("You are not authorized to access this resource.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred while getting items.");
            }
        }

        [HttpPost]
        [Route("add")]
        [Authorize]

        public async Task<IActionResult> CreateItem([FromBody] Item request)
        {
            try
            {
                if (request.Warehouseid <= 0)
                {
                    return BadRequest("Invalid warehouse ID");
                }

                var item = new Item
                {
                    Itemname = request.Itemname,
                    Itemdescription = request.Itemdescription,
                    Quantity = request.Quantity,
                    Warehouseid = request.Warehouseid
                };

                await _itemService.CreateItem(item);
                return Ok("Created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("update")]
        [Authorize]

        public async Task<IActionResult> UpdateItem([FromBody] Item request)
        {
            try
            {
                if (request.Itemid <= 0)
                {
                    return BadRequest("Item ID is required");
                }

                if (request.Warehouseid <= 0)
                {
                    return BadRequest("Invalid warehouse ID");
                }

                await _itemService.UpdateItem(request);

                return Ok("Item updated successfully");
            }
            catch (OracleException ex) when (ex.Number == 20001)
            {
                return BadRequest("Item does not exist");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                await _itemService.DeleteItem(id);
                return Ok("Item deleted successfully");
            }

            catch (OracleException ex) when (ex.Number == 20001)
            {
                return BadRequest("Item does not exist");
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
