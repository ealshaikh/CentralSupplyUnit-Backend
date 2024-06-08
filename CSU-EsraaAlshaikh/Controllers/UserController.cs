namespace CSU_EsraaAlshaikh.Controllers
{
    using CSU_Core.Models;
    using CSU_Core.Service;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Oracle.ManagedDataAccess.Client;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("list")]
        [Authorize]

        public async Task<ActionResult<IEnumerable<User>>> GetAllUers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                if (users.Count() == 0)
                    return Ok(new { Message = "No users found.", Users = new List<User>() });
                return Ok(new { Message = "Users retrieved successfully.", Users = users });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("You are not authorized to access this resource.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred while getting users.");
            }
        }

        [HttpPost]
        [Route("add")]
        [Authorize]

        public async Task<IActionResult> CreateUser([FromBody] User request)
        {
            try
            {
                if (request.Roleid <= 0)
                {
                    return BadRequest("Invalid role ID");
                }

                var user = new User
                {
                    Firstname = request.Firstname,
                    Lastname = request.Lastname,
                    Email = request.Email,
                    Password = request.Password,
                    Roleid = request.Roleid
                };

                await _userService.CreateUser(user);
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

        public async Task<IActionResult> UpdateUser([FromBody] User request)
        {
            try
            {
                if (request.Roleid <= 0)
                {
                    return BadRequest("Role ID is required");
                }

                if (request.Userid <= 0)
                {
                    return BadRequest("User ID is required");
                }

                await _userService.UpdateUser(request);

                return Ok("User updated successfully");
            }
            catch (OracleException ex) when (ex.Number == 20001)
            {
                return BadRequest("User does not exist");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return Ok("User deleted successfully");
            }

            catch (OracleException ex) when (ex.Number == 20001)
            {
                return BadRequest("User does not exist");
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getRoleNameByID/{roleId}")]

        public async Task<IActionResult> GetRoleName(int roleId)
        {
            try
            {
                var roleName = await _userService.GetRoleName(roleId);
                if (string.IsNullOrEmpty(roleName))
                {
                    return NotFound("Role not found");
                }
                return Ok(roleName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
