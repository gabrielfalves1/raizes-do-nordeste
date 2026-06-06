using Microsoft.EntityFrameworkCore;
using raizes_do_nordeste.Data;
using raizes_do_nordeste.DTOs;
using raizes_do_nordeste.Entities;
using raizes_do_nordeste.Enums;
using raizes_do_nordeste.Exceptions;

namespace raizes_do_nordeste.Services
{
    public class PagamentoService : IPagamentoService
    {
        private readonly ApplicationDbContext _context;
        private static readonly Random _random = new();

        public PagamentoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagamentoResponse> ProcessarPagamentoMockAsync(Guid pedidoId, MetodoPagamentoEnum metodo)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Pagamento)
                .FirstOrDefaultAsync(p => p.Id == pedidoId);

            if (pedido is null)
                throw new RegraNegocioException("Pedido não encontrado.");

            if (pedido.Pagamento is not null && pedido.Pagamento.Status == StatusPagamentoEnum.Aprovado)
                throw new RegraNegocioException("Este pedido já possui um pagamento aprovado.");

            if (pedido.Status == StatusPedidoEnum.Cancelado)
                throw new RegraNegocioException("Não é possível processar pagamento de um pedido cancelado.");

            // Mock: 85% de aprovação, 15% de recusa
            var aprovado = _random.NextDouble() > 0.15;

            var statusPagamento = aprovado ? StatusPagamentoEnum.Aprovado : StatusPagamentoEnum.Recusado;
            var agora = DateTime.UtcNow;

            if (pedido.Pagamento is null)
            {
                var pagamento = new Pagamento
                {
                    PedidoId = pedidoId,
                    Status = statusPagamento,
                    Metodo = metodo,
                    Gateway = "MockGateway",
                    TransactionId = aprovado ? Guid.NewGuid().ToString("N") : null,
                    DataPagamento = aprovado ? agora : null,
                    ValorPago = aprovado ? pedido.ValorTotal : 0
                };
                _context.Pagamentos.Add(pagamento);
                pedido.Pagamento = pagamento;
            }
            else
            {
                pedido.Pagamento.Status = statusPagamento;
                pedido.Pagamento.Metodo = metodo;
                pedido.Pagamento.TransactionId = aprovado ? Guid.NewGuid().ToString("N") : null;
                pedido.Pagamento.DataPagamento = aprovado ? agora : null;
                pedido.Pagamento.ValorPago = aprovado ? pedido.ValorTotal : 0;
            }

            if (aprovado)
                pedido.Status = StatusPedidoEnum.EmPreparo;

            pedido.DataAtualizacao = agora;

            await _context.SaveChangesAsync();

            return new PagamentoResponse
            {
                Id = pedido.Pagamento!.Id,
                PedidoId = pedidoId,
                Status = statusPagamento,
                Metodo = metodo,
                TransactionId = pedido.Pagamento.TransactionId,
                DataPagamento = pedido.Pagamento.DataPagamento,
                ValorPago = pedido.Pagamento.ValorPago,
                StatusPedido = pedido.Status
            };
        }
    }
}
