using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.Interfaces;

namespace SmartPlate.Controllers;

[ApiController]
[Route("userinsights")]
public class UserInsightsController : ControllerBase
{
    private readonly IUserDataInsightsCreateCase _userDataInsightsCreateCase;
    private readonly IUserDataInsightsByUserIdCase _userDataInsightsByUserIdCase;
    private readonly IRegisterUserDataInsightsRulesCase _registerUserDataInsightsRulesCase;
    public UserInsightsController(IUserDataInsightsCreateCase userDataInsightsCreateCase,
    IUserDataInsightsByUserIdCase userDataInsightsByUserIdCase, IRegisterUserDataInsightsRulesCase registerUserDataInsightsRulesCase)
    {
        _userDataInsightsCreateCase = userDataInsightsCreateCase;
        _userDataInsightsByUserIdCase = userDataInsightsByUserIdCase;
        _registerUserDataInsightsRulesCase = registerUserDataInsightsRulesCase;
    }

    [HttpPost("userinsights")]
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

    [HttpGet("userinsights")]
    [Authorize]
    public async Task<IActionResult> GetUserDataInsights()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");

        if (userIdClaim is null) return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        var data = await _userDataInsightsByUserIdCase.ExecuteAsync(userId);
        return Ok(data);
    }

    [HttpPost("userinsights-rules")]
    [Authorize]
    public async Task<IActionResult> RegisterUserDataInsightsRules(UserDataInsightRequest userDataInsightRequest)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");

            if (userIdClaim is null) return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            var data = await _registerUserDataInsightsRulesCase.ExecuteAsync(userId, userDataInsightRequest);
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
