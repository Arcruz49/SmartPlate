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
    private IGetUserMetricsCase _getUserMetricsCase;
    public UserMetricsController(IUserMealMetricsCase userMealMetricsCase, IGetUserMetricsCase getUserMetricsCase)
    {
        _userMealMetricsCase = userMealMetricsCase;        
        _getUserMetricsCase = getUserMetricsCase;        
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

    [HttpGet("userbodymetrics")]
    [Authorize]
    public async Task<IActionResult> GetUserBodyMetrics()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");

        if (userIdClaim is null) return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        var metrics = await _getUserMetricsCase.ExecuteAsync(userId);

        return Ok(metrics);
    }
    
}
