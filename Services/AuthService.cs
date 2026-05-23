using Microsoft.EntityFrameworkCore;
using raizes_do_nordeste.Data;
using raizes_do_nordeste.DTOs;
using raizes_do_nordeste.Entities;
using raizes_do_nordeste.Enums;

namespace raizes_do_nordeste.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthService(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<AuthResult> RegistrarAsync(RegisterRequest request)
        {
            var emailExistente = await _context.Usuarios.AnyAsync(u => u.Email == request.Email);
            if (emailExistente)
                return new AuthResult { Sucesso = false, Mensagem = "Este e-mail já está cadastrado no sistema." };

            string senhaCriptografada = BCrypt.Net.BCrypt.HashPassword(request.Senha);

            var novoUsuario = new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                SenhaHash = senhaCriptografada,
                Telefone = request.Telefone,
                DataCadastro = DateTime.UtcNow,
                Status = Status.Ativo,
                Perfil = Perfil.Cliente
            };

            _context.Usuarios.Add(novoUsuario);
            await _context.SaveChangesAsync();

            return new AuthResult { Sucesso = true, Mensagem = "Usuário cadastrado com sucesso!" };
        }

        public async Task<AuthResult> LoginAsync(LoginRequest request, string ipAddress)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash))
                return new AuthResult { Sucesso = false, Mensagem = "E-mail ou senha incorretos." };

            if (usuario.Status != Status.Ativo)
                return new AuthResult { Sucesso = false, Mensagem = "Esta conta de usuário está inativa ou bloqueada." };

            return await GerarESalvarTokensAsync(usuario, ipAddress);
        }

        public async Task<AuthResult> RefreshAsync(RefreshTokenRequest request, string ipAddress)
        {
            var principal = _tokenService.ObterPrincipalDoTokenExpirado(request.AccessToken);
            var emailUsuario = principal.Identity?.Name;

            var refreshTokenSalvo = await _context.RefreshTokens
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(t => t.Token == request.RefreshToken);

            if (refreshTokenSalvo == null || refreshTokenSalvo.Revogado ||
                refreshTokenSalvo.DataExpiracao <= DateTime.UtcNow || refreshTokenSalvo.Usuario.Email != emailUsuario)
            {
                return new AuthResult { Sucesso = false, Mensagem = "Requisição de renovação inválida. Faça login novamente." };
            }

            refreshTokenSalvo.Revogado = true;

            return await GerarESalvarTokensAsync(refreshTokenSalvo.Usuario, ipAddress);
        }

        private async Task<AuthResult> GerarESalvarTokensAsync(Usuario usuario, string ipAddress)
        {
            var accessToken = _tokenService.GerarAccessToken(usuario);
            var refreshTokenString = _tokenService.GerarRefreshToken();

            var novoRefreshToken = new RefreshToken
            {
                Token = refreshTokenString,
                DataCriacao = DateTime.UtcNow,
                DataExpiracao = DateTime.UtcNow.AddDays(7),
                Revogado = false,
                UsuarioId = usuario.Id,
                EnderecoIP = ipAddress
            };

            _context.RefreshTokens.Add(novoRefreshToken);
            await _context.SaveChangesAsync();

            return new AuthResult
            {
                Sucesso = true,
                AccessToken = accessToken,
                RefreshToken = refreshTokenString
            };
        }
    }
}