🔒 Variáveis de Ambiente e Configuração
A API utiliza um sistema desacoplado de gerenciamento de configurações que lê credenciais diretamente de um arquivo externo de ambiente.

Na raiz do projeto, crie um arquivo chamado .env (utilize o arquivo .env.example presente no repositório como referência).

Preencha as propriedades com as credenciais do seu servidor SQL local e com uma chave secreta forte para criptografia dos tokens JWT:

Snippet de código
ConnectionStrings__DefaultConnection=Server=SEU_SERVIDOR\\SQLEXPRESS;Database=RaizesDoNordeste;User Id=SEU_USUARIO;Password=SUA_SENHA;TrustServerCertificate=True;MultipleActiveResultSets=true
Jwt__Key=COLOQUE_UMA_CHAVE_LONGA_E_ALEATORIA_AQUI_DE_PELO_MENOS_256_BITS
Jwt__Issuer=SuaApi
Jwt__Audience=SeusClientes
⚠️ Regra Crítica de Segurança: O arquivo .env contém dados altamente sensíveis de infraestrutura e credenciais. Ele está configurado no arquivo .gitignore e nunca deve ser versionado ou enviado ao repositório público.

🚀 Instalação e Execução Passo a Passo
Siga as instruções abaixo no terminal de comandos do seu sistema operacional na pasta raiz do projeto:

1. Executar as Migrations e Alimentar o Banco de Dados (Seed)
O projeto utiliza Fluent API Data Seeding configurado diretamente nas classes de mapeamento relacional. Ao aplicar as migrations, o banco será criado automaticamente e populado com uma Categoria Padrão (Pratos Regionais), duas Unidades Ativas (Boa Viagem e Riomar) e dois Produtos Regionais (Cuscuz com Charque e Tapioca de Carne de Sol) com IDs (GUIDs) pré-definidos e estáticos para facilitar a validação ponta a ponta.

Gere e aplique o esquema estrutural rodando:

Bash
dotnet ef migrations add InicialEDataSeeding
dotnet ef database update
2. Iniciar a API Localmente
Execute o servidor embutido do Kestrel para escutar as requisições:

Bash
dotnet run
O console exibirá as portas ativas, tipicamente acessíveis em http://localhost:5205 ou similar.

📖 Documentação Interativa da API (Swagger UI)
A API possui documentação de contrato REST em total conformidade com a especificação OpenAPI/Swagger gerada pela biblioteca Swashbuckle. A interface visual permite analisar esquemas de dados, parâmetros obrigatórios, códigos HTTP retornados e testar endpoints interativamente.

URL de Acesso ao Swagger Local: http://localhost:5205/swagger

Rota do JSON de Especificação Técnica: http://localhost:5205/swagger/v1/swagger.json

🧪 Plano de Testes e Cobertura (Postman Collection)
Para atender à exigência regulamentar do projeto, a validação de comportamento foi consolidada em um arquivo executável integrado. O arquivo Raizes_do_Nordeste_Testes.postman_collection.json encontra-se na raiz deste repositório e deve ser importado para o seu Postman.

O plano abrange 10 cenários de testes fundamentais, estruturados em fluxos positivos e negativos com respostas salvas, validando as regras do domínio:

Cenários Positivos (Fluxo Esperado - 6 Testes)
T01 - Login Válido: Autentica o usuário de teste e retorna um AccessToken JWT de 15 minutos.

T02 - Criar Pedido com Sucesso: Envia o payload contendo a Unidade e itens desejados utilizando os GUIDs do Seed. Retorna HTTP 201 e status AGUARDANDO_PAGAMENTO.

T03 - Consultar Pedido Recém-Criado: Executa um GET utilizando o ID dinâmico gerado no passo anterior para validar os cálculos estruturais.

T04 - Pagamento Mock Aprovado: Simula a chamada do gateway externo que liquida o pedido. Altera o status do pedido para RECEBIDO de forma assíncrona.

T05 - Pagamento Mock Recusado: Testa a resiliência do fluxo de pagamento ao simular uma falha do provedor de crédito, mantendo o pedido retido de forma segura.

T06 - Registrar Novo Usuário: Simula o auto-cadastro público de um novo cliente.

Cenários Negativos (Erros e Regras de Negócio - 4 Testes)
T07 - Criar Pedido sem Token: Bate no endpoint protegido sem fornecer as credenciais JWT no header Bearer. Retorna HTTP 401.

T08 - Criar Pedido sem Campo Obrigatório (canalPedido): Passa as propriedades nulas para validar que os DTOs Nullables barram e forçam o preenchimento do canal de origem, retornando HTTP 400.

T09 - Criar Pedido com Unidade Inexistente: Envia um GUID de Unidade falso para testar a proteção relacional do banco. Retorna o JSON padronizado de erro com HTTP 400.

T10 - Criar Pedido com Tipo/Formato Inválido: Envia itens com quantidade negativa (-1) provando que a regra do domínio impede a inserção de dados espúrios no banco.

⚖️ Conformidade com a LGPD, Segurança e Logs
Segurança e Privacidade (Mínimo Técnico Exigido)
Dados Pessoais Protegidos: As senhas dos usuários nunca são salvas em texto puro. O sistema utiliza a biblioteca criptográfica BCrypt para gerar hashes matemáticos irreversíveis (BCrypt.Net.BCrypt.HashPassword), cumprindo o princípio da segurança da LGPD.

Minimização em Responses: As respostas JSON de erros e listagens ocultam dados críticos e sensíveis dos usuários. Os tokens JWT expiram de maneira curta (15 minutos) e o sistema implementa controle robusto de expiração de RefreshTokens gravados de forma unívoca por IP.

Declaração de Auditoria e Logs de Ações Sensíveis
📝 Nota Informativa / Justificativa Técnica: Em total alinhamento com a seção 8.3 e 8.4 do roteiro técnico da disciplina, declara-se que os Logs Automatizados de Infraestrutura/Auditoria persistidos em arquivos ou tabelas não foram implementados nesta versão de MVP. A decisão técnica priorizou o fechamento completo do fluxo crítico ponta a ponta (Pedido -> Pagamento Mock -> Mudança de Status), as validações de dados impeditivas de entrada e o tratamento customizado e padronizado de exceções através da interface IExceptionHandler do .NET 8, blindando a consistência transacional da solução de negócio.