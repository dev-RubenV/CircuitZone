using Microsoft.AspNetCore.Mvc;
using CircuitZone.Data;
using CircuitZone.Entities;
using CircuitZoneConsumerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CircuitZoneConsumerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentosController : ControllerBase
    {
        protected readonly BusinessContext _businessContext;

        public MovimentosController(BusinessContext businessContext)
        {
            _businessContext = businessContext;
        }

        [HttpPost("/entrada-stock")]
        public async Task<IActionResult> AddStock(int productId, int quantity)
        {
            if (quantity <= 0)
            {
                return BadRequest("Inseriu uma quantidade negativa ou zero. Por favor, insira uma quantidade superior a 0.");
            }
            
            var produto = await _businessContext.Produtos.FirstOrDefaultAsync(p => p.Id.Equals(productId));

                if (produto == null)
    {
        return NotFound("Produto não encontrado.");
    }

            var newMovement = new Movimento();

            newMovement.TipoMovimentoId = 1;
            newMovement.DataMovimento = DateTime.Now;
            newMovement.Quantidade = quantity;
            newMovement.UtilizadorId = 1;
            newMovement.ProdutoId = productId;

            produto.IsUpdated = DateTime.Now;
            produto.QuantidadeDisponivel += quantity;

            _businessContext.Movimentos.Add(newMovement);

            var result = await _businessContext.SaveChangesAsync();

            if (result > 0)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("/saida-stock")]
        public async Task<IActionResult> RemoveStock(int productId, int quantity)
        {
            var produto = await _businessContext.Produtos.FirstOrDefaultAsync(p => p.Id.Equals(productId));

            if (quantity == 0)
            {
                return BadRequest($"Selecione um valor superior a zero.");
            }

            if (produto.QuantidadeDisponivel - quantity < 0)
            {
                return BadRequest($"Valor que tentou retirar de stock é superior ao stock atual do produto ({produto.QuantidadeDisponivel}).");
            }

            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }

            var newMovement = new Movimento();

            newMovement.TipoMovimentoId = 2;
            newMovement.DataMovimento = DateTime.Now;
            newMovement.Quantidade = quantity;
            newMovement.UtilizadorId = 1;
            newMovement.ProdutoId = productId;

            produto.IsUpdated = DateTime.Now;
            produto.QuantidadeDisponivel -= quantity;

            _businessContext.Movimentos.Add(newMovement);

            var result = await _businessContext.SaveChangesAsync();

            if (result > 0)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
