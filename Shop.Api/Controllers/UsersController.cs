using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shop.Application.DTOs;
using Shop.Application.Interfaces;
using Shop.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public UsersController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto dto)
    {
        var user = await _userService.RegisterAsync(dto);
        return Ok(new { user.Id, user.Email });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserDto dto)
    {
        var user = await _userService.LoginAsync(dto);
        if (user == null) return Unauthorized(new { message = "Invalid credentials." });

        var token = GenerateToken(user);
        return Ok(new { token });
    }

    private string GenerateToken(User user)
    {
        var key = _configuration["Jwt:Key"] ?? "secret-key";
        var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
