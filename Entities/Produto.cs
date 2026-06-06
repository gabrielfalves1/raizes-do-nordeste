namespace raizes_do_nordeste.Entities
{
    public class Produto
    {
        public Guid Id { get; set; }
        public Guid CategoriaId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public bool Ativo { get; set; }
        public virtual Categoria Categoria { get; set; } = null!;
        public virtual ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
    }
}
