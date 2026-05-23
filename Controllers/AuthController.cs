using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using raizes_do_nordeste.DTOs;
using raizes_do_nordeste.Services;

namespace raizes_do_nordeste.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("registrar")]
        [AllowAnonymous]
        public async Task<IActionResult> Registrar([FromBody] RegisterRequest request)
        {
            var resultado = await _authService.RegistrarAsync(request);

            return StatusCode(StatusCodes.Status201Created, new { mensagem = resultado.Mensagem });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "IP Desconhecido";

            var resultado = await _authService.LoginAsync(request, ipAddress);

            return Ok(new
            {
                AccessToken = resultado.AccessToken,
                RefreshToken = resultado.RefreshToken
            });
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "IP Desconhecido";

            var resultado = await _authService.RefreshAsync(request, ipAddress);

            return Ok(new
            {
                AccessToken = resultado.AccessToken,
                RefreshToken = resultado.RefreshToken
            });
        }
    }
}