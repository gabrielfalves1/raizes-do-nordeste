namespace raizes_do_nordeste.Entities
{
    public class Categoria
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
