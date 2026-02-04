using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.Interfaces;

namespace SmartPlate.Controllers;

[ApiController]
[Route("auth")]
public class LoginController : ControllerBase
{
    private readonly IRegisterUserUseCase _registerUserUseCase;
    private readonly ILoginUseCase _loginUseCase;
    public LoginController(IRegisterUserUseCase registerUserUseCase, ILoginUseCase loginUseCase)
    {
        _registerUserUseCase = registerUserUseCase;
        _loginUseCase = loginUseCase;
    }

    [HttpPost("register")]
    [EnableRateLimiting("RegisterPolicy")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        try
        {
            var user = await _registerUserUseCase.ExecuteAsync(request);
            return Ok(user);
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

    [HttpPost("login")]
    [EnableRateLimiting("LoginPolicy")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var user = await _loginUseCase.ExecuteAsync(request);
            return Ok(user);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }
}
