using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using raizes_do_nordeste.DTOs;
using raizes_do_nordeste.Entities;
using raizes_do_nordeste.Services;

namespace raizes_do_nordeste.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(ApplicationDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // 1. Busca o usuário no banco
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);

            // IMPORTANTE: Aqui você deve comparar a senha usando BCrypt ou similar!
            // Exemplo simplificado: if (usuario == null || !BCrypt.Verify(request.Senha, usuario.SenhaHash))
            if (usuario == null)
                return Unauthorized(new { mensagem = "E-mail ou senha incorretos." });

            // 2. Gera os tokens
            var accessToken = _tokenService.GerarAccessToken(usuario);
            var refreshTokenString = _tokenService.GerarRefreshToken();

            // 3. Salva o Refresh Token no banco de dados usando a sua entidade
            var novoRefreshToken = new RefreshToken
            {
                Token = refreshTokenString,
                DataCriacao = DateTime.UtcNow,
                DataExpiracao = DateTime.UtcNow.AddDays(7), // Dura 7 dias
                Revogado = false,
                UsuarioId = usuario.Id,
                EnderecoIP = HttpContext.Connection.RemoteIpAddress?.ToString()
            };

            _context.RefreshTokens.Add(novoRefreshToken);
            await _context.SaveChangesAsync();

            // 4. Retorna para o front-end
            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenString
            });
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            // 1. Extrai os dados (Claims) do token expirado
            var principal = _tokenService.ObterPrincipalDoTokenExpirado(request.AccessToken);
            var emailUsuario = principal.Identity?.Name;

            // 2. Busca o Refresh Token no banco, incluindo os dados do dono dele
            var refreshTokenSalvo = await _context.RefreshTokens
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(t => t.Token == request.RefreshToken);

            // 3. Validações severas de segurança
            if (refreshTokenSalvo == null ||
                refreshTokenSalvo.Revogado ||
                refreshTokenSalvo.DataExpiracao <= DateTime.UtcNow ||
                refreshTokenSalvo.Usuario.Email != emailUsuario)
            {
                return BadRequest("Requisição de renovação inválida. Faça login novamente.");
            }

            // 4. Rotação de Token: Revoga o antigo
            refreshTokenSalvo.Revogado = true;

            // 5. Gera os novos tokens
            var novoAccessToken = _tokenService.GerarAccessToken(refreshTokenSalvo.Usuario);
            var novoRefreshTokenString = _tokenService.GerarRefreshToken();

            // 6. Salva o novo token no banco
            var novoRefreshToken = new RefreshToken
            {
                Token = novoRefreshTokenString,
                DataCriacao = DateTime.UtcNow,
                DataExpiracao = DateTime.UtcNow.AddDays(7),
                UsuarioId = refreshTokenSalvo.UsuarioId,
                EnderecoIP = HttpContext.Connection.RemoteIpAddress?.ToString()
            };

            _context.RefreshTokens.Add(novoRefreshToken);
            await _context.SaveChangesAsync(); // Atualiza o revogado e insere o novo numa tacada só

            return Ok(new
            {
                AccessToken = novoAccessToken,
                RefreshToken = novoRefreshTokenString
            });
        }
    }
}