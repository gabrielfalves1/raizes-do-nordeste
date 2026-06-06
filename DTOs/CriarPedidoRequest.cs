using raizes_do_nordeste.Enums;

namespace raizes_do_nordeste.DTOs
{
    public class ItemPedidoRequest
    {
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }

    public class CriarPedidoRequest
    {
        public Guid? UnidadeId { get; set; }
        public CanalPedidoEnum? CanalPedido { get; set; }
        public string FormaPagamento { get; set; }
        public List<ItemPedidoRequest> Itens { get; set; }
    }
}
