using Microsoft.EntityFrameworkCore;
using raizes_do_nordeste.Data;
using raizes_do_nordeste.DTOs;
using raizes_do_nordeste.Entities;
using raizes_do_nordeste.Enums;
using raizes_do_nordeste.Exceptions;

namespace raizes_do_nordeste.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly ApplicationDbContext _context;

        public PedidoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PedidoResponse> CriarPedidoAsync(Guid usuarioId, CriarPedidoRequest request)
        {
            if (!request.CanalPedido.HasValue || !Enum.IsDefined(typeof(CanalPedidoEnum), request.CanalPedido.Value))
                throw new RegraNegocioException("Canal do pedido não informado ou inválido.");

            if (!request.UnidadeId.HasValue || request.UnidadeId.Value == Guid.Empty)
                throw new RegraNegocioException("A unidade do pedido é obrigatória.");

            if (request.Itens == null || request.Itens.Count == 0)
                throw new RegraNegocioException("O pedido deve conter ao menos um item.");

            if (request.Itens.Any(i => i.Quantidade <= 0))
                throw new RegraNegocioException("A quantidade de todos os itens deve ser maior que zero.");

            var unidadeExiste = await _context.Unidades
                .AnyAsync(u => u.Id == request.UnidadeId.Value && u.Status == StatusEnum.Ativo);

            if (!unidadeExiste)
                throw new RegraNegocioException("A unidade informada não foi encontrada ou está inativa.");

            var produtoIds = request.Itens.Select(i => i.ProdutoId).Distinct().ToList();
            var produtos = await _context.Produtos
                .Where(p => produtoIds.Contains(p.Id) && p.Ativo)
                .ToListAsync();

            if (produtos.Count != produtoIds.Count)
                throw new RegraNegocioException("Um ou mais produtos não foram encontrados ou estão inativos.");

            var agora = DateTime.UtcNow;

            var pedido = new Pedido
            {
                UsuarioId = usuarioId,
                UnidadeId = request.UnidadeId.Value,
                CanalPedido = request.CanalPedido.Value,
                Status = StatusPedidoEnum.Recebido,
                DataCriacao = agora,
                DataAtualizacao = agora
            };

            var itensPedido = request.Itens.Select(itemReq =>
            {
                var produto = produtos.First(p => p.Id == itemReq.ProdutoId);
                return new ItemPedido
                {
                    PedidoId = pedido.Id,
                    ProdutoId = produto.Id,
                    Quantidade = itemReq.Quantidade,
                    PrecoUnitario = produto.Preco,
                    Subtotal = produto.Preco * itemReq.Quantidade
                };
            }).ToList();

            pedido.Itens = itensPedido;
            pedido.ValorTotal = itensPedido.Sum(i => i.Subtotal);

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return MapearParaResponse(pedido, produtos);
        }

        public async Task<PedidoResponse?> ObterPedidoPorIdAsync(Guid pedidoId)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id == pedidoId);

            if (pedido is null) return null;

            return MapearParaResponse(pedido, pedido.Itens.Select(i => i.Produto).ToList());
        }

        private static PedidoResponse MapearParaResponse(Pedido pedido, List<Produto> produtos)
        {
            return new PedidoResponse
            {
                Id = pedido.Id,
                CanalPedido = pedido.CanalPedido,
                Status = pedido.Status,
                ValorTotal = pedido.ValorTotal,
                DataCriacao = pedido.DataCriacao,
                Itens = pedido.Itens.Select(i =>
                {
                    var produto = produtos.FirstOrDefault(p => p.Id == i.ProdutoId);
                    return new ItemPedidoResponse
                    {
                        Id = i.Id,
                        ProdutoId = i.ProdutoId,
                        NomeProduto = produto?.Nome ?? string.Empty,
                        Quantidade = i.Quantidade,
                        PrecoUnitario = i.PrecoUnitario,
                        Subtotal = i.Subtotal
                    };
                }).ToList()
            };
        }
    }
}
