using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using raizes_do_nordeste.DTOs;
using raizes_do_nordeste.Services;
using System.Security.Claims;

namespace raizes_do_nordeste.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        private readonly IPagamentoService _pagamentoService;

        public PedidosController(IPedidoService pedidoService, IPagamentoService pagamentoService)
        {
            _pedidoService = pedidoService;
            _pagamentoService = pagamentoService;
        }

        [HttpPost]
        public async Task<IActionResult> CriarPedido([FromBody] CriarPedidoRequest request)
        {
            var usuarioIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(usuarioIdStr, out var usuarioId))
                return Unauthorized();

            var response = await _pedidoService.CriarPedidoAsync(usuarioId, request);
            return CreatedAtAction(nameof(ObterPedido), new { id = response.Id }, response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPedido(Guid id)
        {
            var response = await _pedidoService.ObterPedidoPorIdAsync(id);
            if (response is null) return NotFound();
            return Ok(response);
        }

        [HttpPost("{id:guid}/pagamento")]
        public async Task<IActionResult> ProcessarPagamento(Guid id, [FromBody] ProcessarPagamentoRequest request)
        {
            var response = await _pagamentoService.ProcessarPagamentoMockAsync(id, request.Metodo);
            return Ok(response);
        }
    }
}
