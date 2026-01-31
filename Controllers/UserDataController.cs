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
    private readonly IUserDataByUserIdCase _userDataByUserIdCase;
    private readonly IUserDataInsightsCreateCase _userDataInsightsCreateCase;
    public UserDataController(IUserDataCreateCase userDataCreate, IUserDataByUserIdCase userDataByUserIdCase, IUserDataInsightsCreateCase userDataInsightsCreateCase)
    {
        _userDataCreate = userDataCreate;
        _userDataByUserIdCase = userDataByUserIdCase;
        _userDataInsightsCreateCase = userDataInsightsCreateCase;
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

    [HttpGet("userdata")]
    [Authorize]
    public async Task<IActionResult> GetUserData()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");

        if (userIdClaim is null) return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        var data = await _userDataByUserIdCase.ExecuteAsync(userId);
        return Ok(data);
    }

    [HttpPost("userdatainsights")]
    [Authorize]
    public async Task<IActionResult> RegisterUserDataInsights()
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");

            if (userIdClaim is null) return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            var data = await _userDataInsightsCreateCase.ExecuteAsync(userId);
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
