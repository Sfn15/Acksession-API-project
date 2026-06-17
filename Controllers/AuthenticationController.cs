using Microsoft.AspNetCore.Mvc;
using FinalProject.Dtos;
using FinalProject.Services;

namespace FinalProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
    {
        AuthResponseDto? response = await _authService.LoginAsync(loginDto);
        if(response == null)
        {
            return Unauthorized("Invalid email or password.");
        }
        else
        {
            return Ok(response);
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto registerDto)
    {
        AuthResponseDto? response = await _authService.RegisterAsync(registerDto);
        if(response == null)
        {
            return BadRequest("Email or username already taken.");
        }
        else
        {
            return Ok(response);
        }
    }
}