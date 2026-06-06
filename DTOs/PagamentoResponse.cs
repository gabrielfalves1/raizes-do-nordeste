using raizes_do_nordeste.Enums;

namespace raizes_do_nordeste.DTOs
{
    public class ProcessarPagamentoRequest
    {
        public MetodoPagamentoEnum Metodo { get; set; }
    }

    public class PagamentoResponse
    {
        public Guid Id { get; set; }
        public Guid PedidoId { get; set; }
        public StatusPagamentoEnum Status { get; set; }
        public MetodoPagamentoEnum Metodo { get; set; }
        public string? TransactionId { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal ValorPago { get; set; }
        public StatusPedidoEnum StatusPedido { get; set; }
    }
}
