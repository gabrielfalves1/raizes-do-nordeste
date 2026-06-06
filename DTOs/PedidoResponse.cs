using raizes_do_nordeste.Enums;

namespace raizes_do_nordeste.DTOs
{
    public class ItemPedidoResponse
    {
        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class PedidoResponse
    {
        public Guid Id { get; set; }
        public CanalPedidoEnum CanalPedido { get; set; }
        public StatusPedidoEnum Status { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataCriacao { get; set; }
        public List<ItemPedidoResponse> Itens { get; set; } = new();
    }
}
