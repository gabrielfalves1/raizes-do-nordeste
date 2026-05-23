namespace raizes_do_nordeste.DTOs
{
    public class AuthResult
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}