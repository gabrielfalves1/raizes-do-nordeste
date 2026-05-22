using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace raizes_do_nordeste.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutoController(ApplicationDbContext dbContext) : ControllerBase
    {
        [HttpGet]
        [Route("produtos")]
        public List<Produto> Get()
        {
            return dbContext.Produtos.ToList();
        }


        [HttpPost]
        [Route("produtos")]
        public IActionResult CriarProduto([FromBody] Produto produto)
        {
            dbContext.Produtos.Add(produto);
            dbContext.SaveChanges();
            return NoContent();
        }
    }


}

