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

        [HttpGet("/get-movimentos")]
        public async Task<IActionResult> GetMovimentos()
        {
            var movimentosTable = await _businessContext.Movimentos.Select( m => new MovementsModel
            {
                Id = m.Id,
                Quantidade = m.Quantidade,
                DataMovimento = m.DataMovimento,
                TipoMovimento = m.TipoMovimento.Tipo,
                TipoMovimentoId = m.TipoMovimentoId,
                ProdutoNome = m.Produto.Nome,
                ProdutoDescricao = m.Produto.Descricao,
                ProdutoId = m.ProdutoId,
                ProdutoPreco = m.Produto.Preco,
                ImagemThumbnail = m.Produto.Imagens.Any() ? m.Produto.Imagens.Select(m => m.Url).FirstOrDefault().ToString()
                                  : "/Images/no-image.png",

                MovimentoTotal = m.Quantidade * m.Produto.Preco,
            }
                ).ToListAsync();

            if (!movimentosTable.Any())
                return NotFound();
            return Ok(movimentosTable);
        }

        [HttpGet("/get-movimento")]
        public async Task<IActionResult> GetSingleMovimento(int id)
        {
            var movimentosTable = await _businessContext.Movimentos.Where(m => m.Id.Equals(id))
                .Select(m => new MovementsModel
            {
                Id = m.Id,
                Quantidade = m.Quantidade,
                DataMovimento = m.DataMovimento,
                TipoMovimento = m.TipoMovimento.Tipo,
                TipoMovimentoId = m.TipoMovimentoId,
                ProdutoNome = m.Produto.Nome,
                ProdutoDescricao = m.Produto.Descricao,
                ProdutoId = m.ProdutoId,
                ProdutoPreco = m.Produto.Preco,
                ImagemThumbnail = m.Produto.Imagens.Any() ? m.Produto.Imagens.Select(m => m.Url).FirstOrDefault().ToString()
                                  : "/Images/no-image.png",

                MovimentoTotal = m.Quantidade * m.Produto.Preco,
            }
                ).FirstOrDefaultAsync();

            if (movimentosTable is null)
                return NotFound();
            return Ok(movimentosTable);
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
                return Ok(newMovement);
            return BadRequest("Falha ao adicionar stock");
        }

        [HttpPost("/saida-stock")]
        public async Task<IActionResult> RemoveStock(int productId, int quantity)
        {
            var produto = await _businessContext.Produtos.FirstOrDefaultAsync(p => p.Id.Equals(productId));

            if (quantity <= 0)
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
                return Ok(newMovement);
            return BadRequest("Falha ao retirar stock");
        }
    }
}
