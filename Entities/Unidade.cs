using raizes_do_nordeste.Enums;

namespace raizes_do_nordeste.Entities
{
    public class Unidade
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public StatusEnum Status { get; set; }
    }
}
