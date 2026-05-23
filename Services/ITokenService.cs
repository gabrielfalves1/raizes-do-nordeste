using raizes_do_nordeste.Entities;
using System.Security.Claims;

namespace raizes_do_nordeste.Services
{
    public interface ITokenService
    {
        string GerarAccessToken(Usuario usuario);
        string GerarRefreshToken();
        ClaimsPrincipal ObterPrincipalDoTokenExpirado(string token);
    }
}