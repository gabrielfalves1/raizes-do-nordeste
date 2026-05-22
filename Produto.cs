namespace raizes_do_nordeste
{
    public class Produto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public float Valor { get; set; }
    }
}
