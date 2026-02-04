using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.Interfaces;

namespace SmartPlate.Controllers;

[ApiController]
[Route("metrics")]
public class UserMetricsController : ControllerBase
{
    private IUserMealMetricsCase _userMealMetricsCase;
    public UserMetricsController(IUserMealMetricsCase userMealMetricsCase)
    {
        _userMealMetricsCase = userMealMetricsCase;        
    }

    [HttpGet("mealmetrics")]
    [Authorize]
    public async Task<IActionResult> GetUserMealMetrics([FromQuery] UserMealsMetricsRequest request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");

        if (userIdClaim is null) return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        var metrics = await _userMealMetricsCase.ExecuteAsync(userId, request);

        return Ok(metrics);
    }
    
}
