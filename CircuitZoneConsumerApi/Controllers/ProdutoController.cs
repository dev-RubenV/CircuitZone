using CircuitZone.Data;
using CircuitZone.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CircuitZone.Shared.Models;

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
            return await _businessContext.Produtos.ToListAsync();
        }

        //

        [HttpPost("/addprodutos")]
        public async Task<IActionResult> AddProdutos(ProductModel productModel)
        {
            var produto = await _businessContext.Produtos.FirstOrDefaultAsync(t => t.Id.Equals(productModel.Id));

            if (produto is not null)
                return BadRequest();

            var newProduto = new Produto();
            newProduto.Nome = productModel.Nome;
            newProduto.Descricao = productModel.Descricao;
            //newProduto.Marca = productModel.Marca;
            //newProduto.Categoria = productModel.Categoria;
            newProduto.Preco = productModel.Preco;
            newProduto.CodigoEAN = productModel.CodigoEAN;
            newProduto.QuantidadeDisponivel = productModel.QuantidadeDisponivel;
            //newProduto.Imagem = productModel.Imagem;

            _businessContext.Produtos.Add(newProduto);

            var result = await _businessContext.SaveChangesAsync();

            if (result.Equals(1))
                return Ok();

            return BadRequest();
        }

        [HttpPut("/editproduto")]
        public async Task<IActionResult> EditProduto(ProductModel productModel)
        {
            var produto = await _businessContext.Produtos.FirstOrDefaultAsync(t => t.Id.Equals(productModel.Id));

            if (produto is null)
                return BadRequest();

            produto.Nome = productModel.Nome;
            produto.Descricao = productModel.Descricao;
            //produto.Marca = productModel.Marca; 
            //produto.Categoria = productModel.Categoria;
            produto.Preco = productModel.Preco;
            produto.QuantidadeDisponivel = productModel.QuantidadeDisponivel;
            //produto.Imagem = productModel.Imagem;

            var result = await _businessContext.SaveChangesAsync();

            if (result.Equals(1))
                return Ok();

            return BadRequest();
        }
    }
}
