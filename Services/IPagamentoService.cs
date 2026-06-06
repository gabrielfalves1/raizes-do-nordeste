using raizes_do_nordeste.DTOs;
using raizes_do_nordeste.Enums;

namespace raizes_do_nordeste.Services
{
    public interface IPagamentoService
    {
        Task<PagamentoResponse> ProcessarPagamentoMockAsync(Guid pedidoId, MetodoPagamentoEnum metodo);
    }
}
