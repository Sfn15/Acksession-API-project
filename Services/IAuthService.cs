using FinalProject.Dtos;

namespace FinalProject.Services;

public interface IAuthService
{
    public Task<AuthResponseDto?> RegisterAsync(RegisterDto dto);
    public Task<AuthResponseDto?> LoginAsync(LoginDto dto);
    
}