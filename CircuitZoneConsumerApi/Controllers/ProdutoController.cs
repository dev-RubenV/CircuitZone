using CircuitZone.Data;
using CircuitZone.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CircuitZoneConsumerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        protected readonly BusinessContext _businessContext;

        public ProdutoController(BusinessContext businessContext)
        {
            _businessContext = businessContext;
        }

        [HttpGet("/produtos")]
        public async Task<List<Produto>> GetProduto()
        {
            return await _businessContext.Produtos.Include(p => p.Imagens).Include(p => p.Categoria).Include(p => p.Marca).ToListAsync();
        }
    }
}
