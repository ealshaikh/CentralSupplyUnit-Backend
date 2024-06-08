namespace CSU_EsraaAlshaikh.Controllers
{
    using CSU_Core.Models;
    using CSU_Core.Service;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Oracle.ManagedDataAccess.Client;

    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            this._roleService = roleService;
        }

        [HttpGet]
        [Route("list")]
        [Authorize]

        public async Task<ActionResult<IEnumerable<Role>>> GetAllRoles()
        {
            try
            {
                var roles = await _roleService.GetAllRoles();
                if (roles.Count() == 0)
                    return Ok(new { Message = "No roles found.", Roles = new List<Role>() });
                return Ok(new { Message = "Roles retrieved successfully.", Roles = roles });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("You are not authorized to access this resource.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred while getting roles.");
            }
        }

        [HttpPost]
        [Route("add")]
        [Authorize]

        public async Task<IActionResult> CreateRole([FromBody] Role request)
        {
            try
            {
                var role = new Role
                {
                    Name = request.Name
                };

                await _roleService.CreateRole(role);
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

        public async Task<IActionResult> UpdateRole([FromBody] Role request)
        {
            try
            {
                if (request.Roleid <= 0)
                {
                    return BadRequest("Role ID is required");
                }

                await _roleService.UpdateRole(request);

                return Ok("Role updated successfully");
            }
            catch (OracleException ex) when (ex.Number == 20001)
            {
                return BadRequest("Role does not exist");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                await _roleService.DeleteRole(id);
                return Ok("Role deleted successfully");
            }

            catch (OracleException ex) when (ex.Number == 20001)
            {
                return BadRequest("Role does not exist");
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
