using raizes_do_nordeste.DTOs;

namespace raizes_do_nordeste.Services
{
    public interface IPedidoService
    {
        Task<PedidoResponse> CriarPedidoAsync(Guid usuarioId, CriarPedidoRequest request);
        Task<PedidoResponse?> ObterPedidoPorIdAsync(Guid pedidoId);
    }
}
