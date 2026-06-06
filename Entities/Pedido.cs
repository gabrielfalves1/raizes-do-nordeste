using raizes_do_nordeste.Enums;

namespace raizes_do_nordeste.Entities
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid? UnidadeId { get; set; }
        public CanalPedidoEnum CanalPedido { get; set; }
        public StatusPedidoEnum Status { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;
        public virtual Unidade? Unidade { get; set; }
        public virtual ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
        public virtual Pagamento? Pagamento { get; set; }
    }
}
