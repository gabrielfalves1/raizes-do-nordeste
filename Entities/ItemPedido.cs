namespace raizes_do_nordeste.Entities
{
    public class ItemPedido
    {
        public Guid Id { get; set; }
        public Guid PedidoId { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public virtual Pedido Pedido { get; set; } = null!;
        public virtual Produto Produto { get; set; } = null!;
    }
}
