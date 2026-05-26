# raizes-do-nordeste

## Variáveis de ambiente

Este projeto agora carrega um arquivo `.env` na inicialização e também aceita variáveis de ambiente normais do sistema.

Crie um arquivo `.env` na raiz do projeto com base em `.env.example`:

```env
ConnectionStrings__DefaultConnection=Server=SEU_SERVIDOR\SQLEXPRESS;Database=RaizesDoNordeste;User Id=SEU_USUARIO;Password=SUA_SENHA;TrustServerCertificate=True;MultipleActiveResultSets=true
Jwt__Key=COLOQUE_UMA_CHAVE_LONGA_E_ALEATORIA_AQUI
Jwt__Issuer=SuaApi
Jwt__Audience=SeusClientes
```

Regras importantes:

- `ConnectionStrings__DefaultConnection` substitui `ConnectionStrings:DefaultConnection`
- `Jwt__Key`, `Jwt__Issuer` e `Jwt__Audience` substituem `Jwt:Key`, `Jwt:Issuer` e `Jwt:Audience`
- O arquivo `.env` não deve ser versionado
