using raizes_do_nordeste.DTOs;

namespace raizes_do_nordeste.Services
{
    public interface IAuthService
    {
        Task<AuthResult> RegistrarAsync(RegisterRequest request);
        Task<AuthResult> LoginAsync(LoginRequest request, string ipAddress);
        Task<AuthResult> RefreshAsync(RefreshTokenRequest request, string ipAddress);
    }
}
