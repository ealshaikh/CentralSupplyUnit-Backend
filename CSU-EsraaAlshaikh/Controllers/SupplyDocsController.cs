namespace CSU_EsraaAlshaikh.Controllers
{
    using CSU_Core.Models;
    using CSU_Core.Service;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Oracle.ManagedDataAccess.Client;

    [Route("api/[controller]")]
    [ApiController]
    public class SupplyDocsController : ControllerBase
    {
        private readonly ISupplyDocsService _supplyDocsService;

        public SupplyDocsController(ISupplyDocsService supplyDocsService)
        {
            _supplyDocsService = supplyDocsService;
        }

        [HttpGet]
        [Route("list")]
        [Authorize]


        public async Task<ActionResult<IEnumerable<Supplydocument>>> GetAllSupplydocuments()
        {
            try
            {
                var Supplydocument = await _supplyDocsService.GetAllSupplydocuments();
                if (Supplydocument.Count() == 0)
                    return Ok(new { Message = "No supply documents found.", Supplydocuments = new List<Supplydocument>() });
                return Ok(new { Message = "Items retrieved successfully.", Supplydocuments = Supplydocument });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("You are not authorized to access this resource.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred while getting supply documents.");
            }
        }

        [HttpPost]
        [Route("add")]
        [Authorize]

        public async Task<IActionResult> CreateSupplydocument([FromBody] Supplydocument request)
        {
            try
            {
                if (request.Warehouseid <= 0)
                {
                    return BadRequest("Invalid warehouse ID");
                }

                if (request.Itemid <= 0)
                {
                    return BadRequest("Invalid item ID");
                }

                var supplyDocument = new Supplydocument
                {
                    Documentname = request.Documentname,
                    Documentsubject = request.Documentsubject,
                    Createdby = request.Createdby,
                    Warehouseid = request.Warehouseid,
                    Itemid = request.Itemid,
                    Status = request.Status
                };

                await _supplyDocsService.CreateSupplydocument(supplyDocument);
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

        public async Task<IActionResult> UpdateSupplydocument([FromBody] Supplydocument request)
        {
            try
            {

                if (request.Documentid <= 0)
                {
                    return BadRequest("Invalid document ID");
                }

                if (request.Warehouseid <= 0)
                {
                    return BadRequest("Invalid warehouse ID");
                }

                if (request.Itemid <= 0)
                {
                    return BadRequest("Invalid item ID");
                }

                await _supplyDocsService.UpdateSupplydocument(request);

                return Ok("Document updated successfully");
            }
            catch (OracleException ex) when (ex.Number == 20001)
            {
                return BadRequest("Document does not exist");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteSupplydocument(int id)
        {
            try
            {
                await _supplyDocsService.DeleteSupplydocument(id);
                return Ok("Document deleted successfully");
            }

            catch (OracleException ex) when (ex.Number == 20001)
            {
                return BadRequest("Document does not exist");
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
