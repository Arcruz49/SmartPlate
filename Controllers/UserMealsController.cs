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
    private readonly IUserMealsDelete _userMealDelete;
    private readonly IGetUserMealsById _getUserMealsById;
    private readonly IUserMealsRuleCreate _userMealsRuleCreate;
    private readonly IReadMealBarCodeCase _readMealBarCodeCase;
    public UserMealsController(IUserMealsCreate userMealCreate, IGetUserMealsByDay getUserMealsByDay, IUserMealsDelete userMealsDelete, IGetUserMealsById getUserMealsById,
    IUserMealsRuleCreate userMealsRuleCreate, IReadMealBarCodeCase readMealBarCodeCase)
    {
        _userMealCreate = userMealCreate;
        _getUserMealsByDay = getUserMealsByDay;
        _userMealDelete = userMealsDelete;
        _getUserMealsById = getUserMealsById;
        _userMealsRuleCreate = userMealsRuleCreate;
        _readMealBarCodeCase = readMealBarCodeCase;
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

    [HttpGet("usermeal")]
    [Authorize]
    public async Task<IActionResult> GetUserMealByDate([FromQuery] UserMealsDayRequest request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");

        if (userIdClaim is null) return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        var userMealsList = await _getUserMealsByDay.ExecuteAsync(userId, request);

        return Ok(userMealsList);
    }
    
    [HttpGet("usermealById")]
    [Authorize]
    public async Task<IActionResult> GetUserMealById([FromQuery] UserMealsIdRequest request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");

        if (userIdClaim is null) return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        var userMeal = await _getUserMealsById.ExecuteAsync(userId, request);

        return Ok(userMeal);
    }

    [HttpDelete("usermeal")]
    [Authorize]
    public async Task<IActionResult> DeleteUserMeal([FromBody] UserMealsIdRequest request)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");

            if (userIdClaim is null) return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            await _userMealDelete.ExecuteAsync(request.MealId, userId);

            return NoContent();
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

    [HttpPost("usermeal-rules")]
    [Authorize]
    public async Task<IActionResult> RegisterUserMealRules([FromBody] UserMealsRuleRequest request)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");

            if (userIdClaim is null) return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            var data = await _userMealsRuleCreate.ExecuteAsync(userId, request);
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

    [HttpGet("usermeal-barcode")]
    [Authorize]
    public async Task<IActionResult> ReadMealBarCode([FromQuery] UserMealBarCodeRequest request)
    {
        if (string.IsNullOrEmpty(request.Code))
            return BadRequest();

        var mealInfo = await _readMealBarCodeCase.ExecuteAsync(request);

        return Ok(mealInfo);
    }
}
