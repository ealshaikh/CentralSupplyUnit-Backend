namespace CSU_EsraaAlshaikh.Controllers
{
    using CSU_Core.Models;
    using CSU_Core.Service;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Oracle.ManagedDataAccess.Client;

    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpGet]
        [Route("list")]
        [Authorize]

        public async Task<ActionResult<IEnumerable<Warehouse>>> GetAllWarehouses()
        {
            try
            {
                var warehouses = await _warehouseService.GetAllWarehouses();
                if (warehouses.Count() == 0)
                    return Ok(new { Message = "No warehouses found.", Warehouses = new List<Warehouse>() });
                return Ok(new { Message = "Warehouses retrieved successfully.", Warehouses = warehouses });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("You are not authorized to access this resource.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred while getting warehouses.");
            }
        }

        [HttpPost]
        [Route("add")]
        [Authorize]

        public async Task<IActionResult> CreateWarehouse([FromBody] Warehouse request)
        {
            try
            {
                var warehouse = new Warehouse
                {
                    Warehousename = request.Warehousename,
                    Warehousedescription = request.Warehousedescription,
                    Createdby = request.Createdby
                };

                await _warehouseService.CreateWarehouse(warehouse);
                return Ok(new { Message = "Warehouse created successfully." });
            }
            catch (OracleException ex) when (ex.Number == 20002)
            {
                return BadRequest(new { Message = "A warehouse with the same name already exists. Please choose a different name." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred while creating the warehouse.", Details = ex.Message });
            }
        }


        [HttpPut]
        [Route("update")]
        [Authorize]

        public async Task<IActionResult> UpdateWarehouse([FromBody] Warehouse request)
        {
            try
            {
                if (request.Warehouseid <= 0)
                {
                    return BadRequest("Warehouse ID is required");
                }

                await _warehouseService.UpdateWarehouse(request);

                return Ok("Warehouse updated successfully");
            }
            catch (OracleException ex) when (ex.Number == 20001)
            {
                return BadRequest("Warehouse does not exist");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            try
            {
                await _warehouseService.DeleteWarehouse(id);
                return Ok("Warehouse deleted successfully");
            }

            catch (OracleException ex) when (ex.Number == 20001)
            {
                return BadRequest("Warehouse does not exist");
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
