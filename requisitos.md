Olá a todos.
Sejam todos muito bem-vindos!
Esta Atividade Prática tem como objetivo avaliar a capacidade do estudante de analisar um
estudo de caso, modelar dados, definir contratos de API e implementar uma solução de Back-end
que atenda aos principais requisitos funcionais e não funcionais do cenário proposto.
O foco da trilha de Back-end é demonstrar domínio sobre:
• Levantamento e priorização de requisitos (o que é essencial no sistema e por que);
• Modelagem do domínio e da base de dados (DER e entidades, relacionamentos e
integridade);
• Arquitetura e organização do projeto (camadas, separação de responsabilidades,
padrões de projeto quando aplicável);
• Implementação de API REST com endpoints coerentes, contratos bem definidos e
documentação via Swagger/OpenAPI;
• Persistência em banco de dados com consistência entre modelo e implementação
(migrations/ORM, seed quando necessário);
• Regras de negócio do fluxo crítico (por exemplo: pedidos, estoque por unidade,
atualização de status, fidelização e validações);
• Integração simulada de pagamento (mock) para demonstrar o fluxo completo sem
depender de provedores reais;
• Segurança e LGPD (mínimo técnico): autenticação, autorização por perfis/roles,
cuidado com dados pessoais, armazenamento seguro (ex.: hash de senha) e respostas
sem exposição indevida de dados;
• Qualidade e testabilidade: plano de testes com cenários positivos e negativos e
evidência reproduzível (coleção Postman/Insomnia e/ou testes automatizados).
Ao final, espera-se que o estudante seja capaz de apresentar uma solução que execute, seja
reproduzível (README claro), e comprove o domínio dos elementos essenciais de um Back-end
profissional, conforme os requisitos e limitações definidos neste roteiro.
No mais, desejo-lhe excelente atividade prática em nome dos professores
da disciplina de Projeto Multidisciplinar.
Roteiro de Atividade Prática de
Projeto Multidisciplinar – Trilha Back-End
2
SUMÁRIO
OBJETIVO___________________________________________________________________1
ORIENTAÇÕES GERAIS_________________________________________________________3
INTRODUÇÃO E OBJETIVOS (OBRIGATÓRIO)___________________________________________ 3
ANÁLISE E REQUISITOS (OBRIGATÓRIOS) _____________________________________________ 4
DIAGRAMAS (OBRIGATÓRIO) ______________________________________________________ 5
API E ENDPOINTS (OBRIGATÓRIO)___________________________________________________ 6
LGPD, PRIVACIDADE E SEGURANÇA NO BACK-END(OBRIGATÓRIO) ________________________ 8
ENTREGA TÉCNICA (OBRIGATÓRIA)__________________________________________________ 8
PLANO DE TESTES (OBRIGATÓRIO) _________________________________________________ 10
CRITÉRIOS DE ENTREGA E NORMAS DA ABNT (OBRIGATÓRIO) ___________________________ 12
CONCLUSÃO (OBRIGATÓRIO)______________________________________________________ 12
ORIENTAÇÃO FINAL _____________________________________________________________ 12
CRITÉRIOS DE AVALIAÇÃO ________________________________________________________ 13
Critérios de Avaliação (Back-end) - Tabela Consolidada___________________ Erro! Indicador não definido.
ANEXOS_______________________________________________________________________ 18
EXEMPLO DE CASOS DE USO ______________________________________________________________ 18
Modelo (template) para documentar endpoints ______________________________________________ 20
Roteiro de Atividade Prática de
Projeto Multidisciplinar – Trilha Back-End
3
ORIENTAÇÕES GERAIS
INTRODUÇÃO E OBJETIVOS (OBRIGATÓRIO)
Esta atividade simula um cenário real de mercado onde o aluno deve projetar uma solução
robusta para uma rede de lanchonetes em expansão. O foco está na compreensão do negócio, na
tomada de decisão técnica e na entrega de uma API/solução de Back-end que suporte múltiplos
canais (App, Totem e Web).
CUIDADO!
Em programação, não existem dois códigos exatamente iguais. Cada programador organiza seu
código de uma forma diferente, declara variáveis com nomes diferentes, faz comentários
diferentes, gera mensagens aos usuários distintas etc. Por este motivo, não serão aceitos dois
algoritmos idênticos entre alunos (ou iguais à Internet). Caso o corretor observe respostas iguais,
elas serão consideradas como PLÁGIO e será atribuída a NOTA ZERO na questão.
Uso de IA nesta atividade (leia com atenção)
Esta atividade é avaliativa e tem como objetivo verificar seu raciocínio e domínio do conteúdo.
Não é permitido usar ferramentas de IA para gerar a solução, código final, respostas prontas ou
explicações que substituam seu estudo.
Você pode usar IA apenas para: revisar ortografia, organizar texto, explicar conceitos de forma
geral, ou sugerir referências — sem produzir a resposta da atividade.
Se você usar IA em qualquer etapa, declare no final do documento: ferramenta utilizada, prompts
e trechos aproveitados.
Trabalhos com indícios de geração automática e sem evidências do processo será atribuída nota
zero.
Roteiro de Atividade Prática de
Projeto Multidisciplinar – Trilha Back-End
4
ANÁLISE E REQUISITOS (OBRIGATÓRIOS)
O aluno deve listar os requisitos com foco em regras de negócio, persistência, integrações e
segurança (não em telas).
• Requisitos Funcionais (RF): cadastro e autenticação (login), gestão de unidades da rede,
cardápio por unidade, gestão de pedidos (criar, atualizar status, cancelar), controle de
estoque, programa de fidelização e integração com serviço externo de pagamento (mock).
o O projeto deve contemplar, todos os requisitos necessários ao completo
funcionamento do sistema incluindo, ao menos, de forma conceitual, os requisitos
obrigatórios abaixo:
▪ Cadastro e autenticação de usuários (incluindo perfis/roles).
▪ Visualização/consulta de cardápio por unidade (API).
▪ Realização de pedidos (com itens, valores, status).
▪ Atualização do status do pedido (cozinha → pronto → entregue / cancelado).
▪ Controle de estoque (entrada/saída e restrição de venda por indisponibilidade).
▪ Programa de fidelização (pontos e resgate simples, com consentimento).
▪ Promoções/campanhas (ao menos como regra/documentação de como
aplicar).
▪ Solicitação de pagamento via serviço externo (mock) + registro. Você não
deve implementar pagamento real, mas deve representar o fluxo do envio
do pagamento e o retorno da API (status e payload) para o cliente
(App/Totem/Web).
o Multicanalidade (domínio) (Obrigatório)
Conforme o estudo de caso, a solução deve atender múltiplos canais (ex.: APP,
TOTEM, BALCÃO, PICKUP, WEB). Assim, o back-end deve tratar o canal de origem
do pedido como um dado de domínio, registrando-o no pedido e garantindo
rastreabilidade operacional entre canais.
Requisito mínimo:
▪ O pedido deve possuir um campo canalPedido (ENUM), com valores como:
APP, TOTEM, BALCAO, PICKUP, WEB.
▪ A criação de pedido deve exigir o preenchimento de canalPedido.
▪ A API deve permitir consultar/filtrar pedidos por canal
(ex.: query param ?canalPedido=TOTEM).
Objetivo: permitir consolidação e acompanhamento do fluxo de pedidos por canal,
mantendo integração e consistência do atendimento na rede.
Roteiro de Atividade Prática de
Projeto Multidisciplinar – Trilha Back-End
5
• Requisitos Não Funcionais (RNF): segurança (LGPD, controle de acesso, senha com hash,
token), logs/auditoria de ações sensíveis, desempenho em horários de pico, disponibilidade
do sistema, tolerância a falhas na integração de pagamento e documentação
(OpenAPI/Swagger).
DIAGRAMAS (OBRIGATÓRIO)
Esta seção fundamenta a lógica da solução e é requisito essencial para a avaliação da visão
sistêmica.
4.1. Diagrama de Casos de Uso
Elaboração de Diagrama de Casos de Uso com os atores:
• Cliente (App/Web/Totem);
• Atendente (Balcão);
• Cozinha;
• Gerente / Administrador;
• Sistemas Externos de Pagamento (Gateway).
Descrição de Feature (obrigatória): escolha as funcionalidades críticas (ex.: Realizar Pedido
+ Solicitar Pagamento) e detalhe fluxo principal, pré-condições, pós-condições, exceções e regras
de negócio (ex.: idempotência, estoque insuficiente, pagamento negado).
4.2. DER (Diagrama Entidade-Relacionamento) / Modelo de Dados
Apresente o DER do banco de dados com entidades, atributos principais e relacionamentos.
Inclua chaves (PK/FK), cardinalidades e restrições relevantes (ex.: uma unidade possui estoque
próprio; pedido possui itens; pagamento é desacoplado).
4.3. Arquitetura (camadas e separação de responsabilidades) (obrigatório)
Para fins de organização e manutenção do projeto, a solução deve apresentar uma estrutura
por camadas (ou equivalente), deixando explícita a separação de responsabilidades entre:
• Domain (Domínio): entidades, regras de negócio, validações e comportamentos do
domínio (ex.: Pedido, Cliente, Produto, Estoque).
• Application (Aplicação): casos de uso/serviços que orquestram o fluxo do sistema (ex.:
criar pedido, aplicar fidelidade, confirmar pagamento mock, atualizar status).
• Infrastructure (Infraestrutura): persistência (ORM/migrations/repositórios), integrações
(pagamento mock, e-mail/log), e detalhes técnicos.
• API (Interface/Controllers): rotas/endpoints, autenticação/autorização, contratos de
request/response e documentação (Swagger/OpenAPI).
Roteiro de Atividade Prática de
Projeto Multidisciplinar – Trilha Back-End
6
Observação importante: você pode adotar variações como MVC, Clean Architecture ou DDD
simplificado, desde que a separação de responsabilidades fique clara no código e na documentação.
A avaliação considera a coerência e clareza da organização, e não a adesão a um framework
específico.
4.4. Diagrama de Classes (obrigatório) e Sequência/Atividade (recomendado)
Apresente um diagrama de classes (visão de domínio) com as principais entidades/objetos e
relacionamentos. Recomenda-se também um diagrama de sequência ou atividade do fluxo crítico
(Pedido → Pagamento Externo → Atualização de Status).
API E ENDPOINTS (OBRIGATÓRIO)
Descreva os principais endpoints da sua API, organizados por recurso (ex.: /auth, /unidades,
/produtos, /estoque, /pedidos, /fidelidade, /pagamentos). Para cada endpoint, apresente: método
HTTP, rota, autenticação/permissões, parâmetros, exemplo de request/response, códigos de status
e padrão de erro.
5.1 Objetivo desta etapa
Deve-se descrever o contrato da sua API (o “combinado” entre Front-end e Back-end), para
que qualquer pessoa consiga:
1. entender o que cada endpoint faz,
2. saber o que enviar (request) e o que receber (response),
3. validar regras e erros,
4. testar com Postman/Insomnia/Swagger.
Importante: prefira qualidade e clareza. É melhor ter os endpoints bem documentados do que
endpoints sem padrão.
5.2 Organização por recurso
Liste os endpoints agrupando por “recurso” (módulo), por exemplo:
• /auth (login, refresh, logout)
• /usuarios (cadastro, perfil)
• /unidades (listar unidades)
• /produtos (CRUD, consulta)
• /estoque (entradas/saídas, consulta por unidade)
Roteiro de Atividade Prática de
Projeto Multidisciplinar – Trilha Back-End
7
• /pedidos (criar pedido, status, consulta)
• /pagamentos (simulação/mock, confirmação)
• /fidelidade (pontos, saldo, histórico)
5.3 O que você precisa apresentar para CADA endpoint (checklist)
Para cada endpoint, documente:
1. Nome do endpoint (propósito em 1 frase)
2. Método HTTP + rota
3. Autenticação e permissões (ex.: JWT; papel ADMIN/GERENTE/CLIENTE)
4. Parâmetros
o Path params (ex.: /produtos/{id})
o Query params (ex.: ?page=1&limit=10)
5. Body (request) com exemplo JSON
6. Response (sucesso) com exemplo JSON
7. Códigos de status esperados (ex.: 200, 201, 400, 401, 403, 404, 409, 422)
8. Padrão de erro (um JSON padrão para todas as falhas)
5.4 Regras mínimas (para evitar “API bagunçada”)
• URLs no plural: /produtos, /pedidos
• IDs no path: /produtos/{id}
• Paginação em listagens: GET /produtos?page=1&limit=10
• Status code coerente:
o 200 (ok), 201 (criado), 204 (sem conteúdo)
o 400/422 (erro de validação), 401 (não autenticado), 403 (sem permissão)
o 404 (não encontrado), 409 (conflito/regra de negócio)
• Erro padronizado: sempre o mesmo formato JSON
5.5 Campos e padrões mínimos do contrato (Obrigatório)
Nos contratos de pedido, inclua explicitamente o campo canalPedido e mantenha consistência
de validação e erros. Caso canalPedido não seja informado ou seja inválido, a API deve retornar erro
com status apropriado (ex.: 400/422) e mensagem padronizada.
Exemplo de request (Pedido — criação):
{
 "canalPedido": "TOTEM",
 "clienteId": 123,
Roteiro de Atividade Prática de
Projeto Multidisciplinar – Trilha Back-End
8
 "itens": [
 { "produtoId": 10, "quantidade": 2 }
 ],
 "formaPagamento": "MOCK"
}
Exemplo de filtro (listagem):
GET /pedidos?canalPedido=APP&status=AGUARDANDO_PAGAMENTO
LGPD, PRIVACIDADE E SEGURANÇA NO BACK-END
(OBRIGATÓRIO)
A LGPD deve ser demonstrada explicitamente no Back-end: quais dados pessoais são
coletados, para qual finalidade, por qual base legal e como o consentimento é registrado. Inclua
controles mínimos: hashing de senha, autenticação por token, autorização por perfil, logs de acesso
a dados sensíveis, e estratégia de retenção/anônimização quando aplicável.
ENTREGA TÉCNICA (OBRIGATÓRIA)
O aluno deve entregar uma API funcional (rodando localmente) + documentação técnica
completa que permita a correção e teste do sistema.
7.1 Entregáveis obrigatórios:
1. Repositório Git público (GitHub/GitLab/Bitbucket) contendo — obrigatório:
a) Código-fonte do Back-end
b) Estrutura organizada (camadas, módulos, pastas)
c) Histórico de commits (mínimo recomendado: 5 commits com evolução do trabalho)
2. README de execução (passo a passo) — obrigatório:
a) Deve conter, no mínimo:
b) Requisitos (linguagem, versão, banco, dependências)
c) Como configurar variáveis de ambiente (.env.example + instruções)
d) Como instalar dependências
e) Como criar banco e executar migrations (e seed, se houver)
f) Como iniciar a API
g) Como acessar a documentação (Swagger/OpenAPI)
h) Como rodar os testes (se houver)
3. Documentação da API (OpenAPI/Swagger) — obrigatória
a) Pode ser gerada automaticamente pela ferramenta do projeto
b) Deve refletir os endpoints implementados (contrato real)
Roteiro de Atividade Prática de
Projeto Multidisciplinar – Trilha Back-End
9
c) Deve conter exemplos de request/response e códigos de status
4. Coleção de testes Postman/Insomnia (arquivo .json) — obrigatória
a) Deve incluir chamadas do fluxo principal (mínimo viável) e evidências de:
a. autenticação (login e uso do token)
b. criação/consulta/atualização conforme o fluxo escolhido
c. pelo menos 1 cenário de erro (ex.: 401, 404, 409 ou 422)
5. Banco de dados e modelo (DER) — obrigatório
a) Entregar o DER (imagem/PDF) e indicar as principais tabelas e relacionamentos
b) O DER deve ser compatível com o banco usado na API
7.2 Requisito mínimo de funcionamento (MVP obrigatório)
A API precisa rodar e demonstrar pelo menos um fluxo completo e testável, com persistência
em banco:
Fluxo obrigatório (escolher 1 e entregar completo):
• Fluxo A (recomendado): Pedido → Pagamento mock → Atualização de status
o criar pedido
o validar itens / regras básicas
o registrar pagamento mock
o atualizar status do pedido
• Fluxo B: Estoque por unidade
o cadastrar/consultar produto
o movimentar estoque (entrada/saída)
o consultar saldo por unidade
Obrigatório em qualquer fluxo
• Persistência em banco (CRUD real, não só mock em memória)
• Pelo menos 1 tipo de autenticação (ex.: JWT) com perfis/permissões (mesmo que
simples)
• Padrão de erro consistente (JSON padronizado)
Observação importante: Entregar “muitos endpoints” não compensa se o fluxo principal
não estiver fechado e testável.
7.3 Evidências (obrigatório)
No README, incluir links e evidências:
• Link do repositório
• “URL do swagger local” + “rota do swagger” ou instrução para acessar o Swagger
• Arquivo da coleção Postman/Insomnia no repositório
• (Opcional) Link de deploy, se existir
AVISO CRÍTICO (NOTA ZERO)
Independentemente da tecnologia escolhida, o aluno deve garantir que os links estejam
PÚBLICOS e funcionais. Se o avaliador não conseguir acessar repositório/Swagger/coleção
Postman no momento da correção, a pontuação do critério “Entrega Técnica e
Documentação” será 0 (zero).
Roteiro de Atividade Prática de
Projeto Multidisciplinar – Trilha Back-End
10
PLANO DE TESTES (OBRIGATÓRIO)
Definir e evidenciar como a API foi validada, garantindo que o corretor consiga reproduzir os
testes usando a coleção Postman/Insomnia entregue no repositório e/ou o Swagger/OpenAPI.
8.1 Evidência obrigatória
Você deve entregar:
• Coleção Postman/Insomnia (.json) com os testes organizados em pastas (ex.: Auth,
Produtos, Pedidos, Pagamento, Erros); e
• No README, indicar como executar os testes (ordem sugerida, token, ambiente, seed).
Observação: os cenários descritos abaixo devem estar refletidos na coleção (ou seja: não basta
descrever, tem que executar).
8.2 Cenários mínimos (mínimo 10)
Apresentar no mínimo 10 cenários de teste, sendo:
• mínimo 6 positivos (fluxo esperado)
• mínimo 4 negativos (erros/regras)
Cada cenário deve conter:
• ID do teste (T01, T02…)
• Endpoint + método
• Pré-condição (ex.: usuário logado; produto existente; seed aplicado)
• Entrada (path/query/body)
• Saída esperada
o status code
o trechos relevantes do response
• Evidência (nome da requisição na coleção Postman/Insomnia e/ou print do Swagger com
exemplo)
8.3 Os testes devem cobrir obrigatoriamente:
a) Autenticação e autorização
a. Login (token válido)
b. Acesso sem token (401)
c. Acesso com perfil sem permissão (403)
Roteiro de Atividade Prática de
Projeto Multidisciplinar – Trilha Back-End
11
b) Validação de dados
a. Campo obrigatório ausente (400/422)
b. Tipo/formato inválido (ex.: e-mail, quantidade negativa) (400/422)
c) Regras de negócio do fluxo principal
Escolha o fluxo adotado no projeto e cubra ao menos:
a. Pedido com itens válidos → criado (ex.: 201)
b. Pedido com produto/unidade inexistente (404)
c. Pedido com estoque insuficiente (409 ou regra equivalente)
d) Pagamento mock e atualização de status
a. Pagamento mock aprovado → status do pedido atualizado
b. Pagamento mock recusado → status e mensagem coerentes
e) Logs/auditoria (quando implementado)
a. Pelo menos 1 teste evidenciando que uma ação sensível gera registro (ex.: criação
de pedido, mudança de status)
Observação: Se você não implementou logs/auditoria: declarar explicitamente “não
implementado” e justificar (isso impacta nota, mas evita inconsistência).
8.4 Formato recomendado
ID Cenário Endpoint Pré-condição Entrada
Esperado (status + pontos do
response)
Evidência (nome na coleção)
T01 Login válido
POST
/auth/login
usuário
cadastrado
{email,
senha}
200 + accessToken Auth/Login válido
T02 Sem token GET /pedidos — — 401 + erro padrão Pedidos/Listar sem token
… … … … … … …
Roteiro de Atividade Prática de
Projeto Multidisciplinar – Trilha Back-End
12
CRITÉRIOS DE ENTREGA E NORMAS DA ABNT
(OBRIGATÓRIO)
O trabalho deve ser entregue em arquivo único (PDF) e seguir rigorosamente as normas da
ABNT para trabalhos acadêmicos.
• Onde encontrar as normas: No menu lateral esquerdo do AVA, acesse Biblioteca e clique
em Normas ABNT.
• O trabalho deve conter capa, sumário, seções do roteiro e referências.
• Postura Profissional: O projeto deve ser apresentado como uma solução real que se
sustentaria em uma entrevista técnica de mercado.
• Como nomear seu arquivo: Utilize o seu RU seguido de Projeto Back End. Exemplo:
4999657_Projeto_Back_End.PDF
CONCLUSÃO (OBRIGATÓRIO)
Resuma o que foi implementado e o que ficou como proposta, justificando prioridades.
Explique como DER/Classes/Casos de Uso se conectam aos endpoints e ao fluxo crítico entregue.
Descreva como o pagamento mock foi tratado e quais validações/erros foram padronizados. Indique
os principais cuidados de segurança/LGPD e como os testes evidenciam o funcionamento.
ORIENTAÇÃO FINAL
Antes de finalizar e realizar a entrega do trabalho, reflita: “Se este projeto fosse apresentado
em uma entrevista técnica ou empresa, ele se sustentaria?"