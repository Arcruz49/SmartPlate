using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.Interfaces;

namespace SmartPlate.Controllers;

[ApiController]
[Route("usermeals")]
public class UserMealsController : ControllerBase
{
    private readonly IUserMealsCreate _userMealCreate;
    private readonly IGetUserMealsByDay _getUserMealsByDay;
    public UserMealsController(IUserMealsCreate userMealCreate, IGetUserMealsByDay getUserMealsByDay)
    {
        _userMealCreate = userMealCreate;
        _getUserMealsByDay = getUserMealsByDay;
    }

    [HttpPost("usermeal")]
    [Authorize]
    public async Task<IActionResult> RegisterUserMeal([FromBody] UserMealsRequest request)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");

            if (userIdClaim is null) return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            var data = await _userMealCreate.ExecuteAsync(userId, request);
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

    [HttpGet("usermeals")]
    [Authorize]
    public async Task<IActionResult> GetUserMealByDate([FromQuery] UserMealsDayRequest request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");

        if (userIdClaim is null) return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        var userMealsList = await _getUserMealsByDay.ExecuteAsync(userId, request);

        return Ok(userMealsList);
    }
}
