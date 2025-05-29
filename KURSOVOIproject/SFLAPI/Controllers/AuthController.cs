using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFLAPI.DTOs;
using SFLAPI.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    public AuthController(IAuthService auth) => _auth = auth;
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        await _auth.RegisterAsync(dto.Name, dto.Password, dto.Role);
        return Ok();
    }
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if (!await _auth.ValidateAsync(dto.Name, dto.Password, dto.Role))
            return Unauthorized();

        var token = await _auth.GenerateTokenAsync(dto.Name, dto.Role);
        return Ok(new { token });
    }
}
