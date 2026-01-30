using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.Interfaces;

namespace SmartPlate.Controllers;

[ApiController]
[Route("User")]
public class UserDataController : ControllerBase
{
    private readonly IUserDataCreateCase _userDataCreate;
    public UserDataController(IUserDataCreateCase userDataCreate)
    {
        _userDataCreate = userDataCreate;
    }

    [HttpPost("userdata")]
    [Authorize]
    public async Task<IActionResult> RegisterUserData([FromBody] UserDataRequest request)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");

            if (userIdClaim is null) return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            var data = await _userDataCreate.ExecuteAsync(userId, request);
            return Ok(data);
        }
        catch (ArgumentException ex) // Value objects
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
        catch (InvalidOperationException ex) // regras de negocio da user case
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    
}
