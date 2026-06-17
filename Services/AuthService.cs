using FinalProject.Dtos;
using FinalProject.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FinalProject.Services;

public class AuthService : IAuthService
{
    
    private static List<User> _users = new List<User>();
    
    
    private readonly PasswordHasher<User> _hasher = new();
    private readonly IConfiguration _configuration;
    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;

        if (!_users.Any())
        {
            var admin = new User
            {
                Id = 1,
                Email = "admin@test.com",
                Username = "admin",
                Role = "Admin"
            };
            admin.PasswordHash = _hasher.HashPassword(admin, "Admin123!Secure");
            _users.Add(admin);
        }
    }
    public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto)
    {
        bool exists = _users.Any(u => u.Email == dto.email || u.Username == dto.username);
        if (exists)
        {
            return null;
        }

        var user = new User
        {
            Id = _users.Count + 1,
            Email = dto.email,
            Username = dto.username
        };

        user.PasswordHash = _hasher.HashPassword(user, dto.password);
        _users.Add(user);

        string token = GenerateToken(user);
        AuthResponseDto response = new AuthResponseDto
        {
            Token = token,
            Username = user.Username,
            ExpiresAt = DateTime.UtcNow.AddHours(1)
        };
        return response;
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        User? user = _users.FirstOrDefault(u => u.Email == dto.email);

        if(user == null)
        {
            return null;
        }
        
        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.password);
        if (result == PasswordVerificationResult.Failed)
        {
            return null;
        }

        string token = GenerateToken(user);

            AuthResponseDto response = new AuthResponseDto
            {
                Token = token,
                Username = user.Username,
                ExpiresAt = DateTime.UtcNow.AddHours(1)
            };

            return response;
    }

    private string GenerateToken(User user)
{
    var claims = new[]
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role)
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: _configuration["Jwt:Issuer"],
        audience: _configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddHours(1),
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
    }
}