namespace raizes_do_nordeste.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime DataExpiracao { get; set; }
        public bool Revogado { get; set; }
        public string? EnderecoIP { get; set; }

        public Guid UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; } = null!;
    }
}