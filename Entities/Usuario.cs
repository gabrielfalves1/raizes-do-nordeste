using raizes_do_nordeste.Enums;

namespace raizes_do_nordeste.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public PerfilEnum Perfil { get; set; }
        public DateTime DataCadastro { get; set; }
        public StatusEnum Status { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}