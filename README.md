# raizes-do-nordeste

## Variáveis de ambiente

Crie um arquivo `.env` na raiz do projeto com base em `.env.example`:

```env
ConnectionStrings__DefaultConnection=Server=SEU_SERVIDOR\SQLEXPRESS;Database=RaizesDoNordeste;User Id=SEU_USUARIO;Password=SUA_SENHA;TrustServerCertificate=True;MultipleActiveResultSets=true
Jwt__Key=COLOQUE_UMA_CHAVE_LONGA_E_ALEATORIA_AQUI
Jwt__Issuer=SuaApi
Jwt__Audience=SeusClientes
```

Regra importante:
- O arquivo `.env` não deve ser versionado
