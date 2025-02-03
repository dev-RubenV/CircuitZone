using CircuitZone.Data;
using CircuitZone.Entities;
using CircuitZoneConsumerApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

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
        public async Task<IActionResult> GetProduct()
        {
            var productTable = await _businessContext.Produtos
                .Where(p => p.IsDeleted == false)
                .Select(p => new ProductModel
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Descricao = p.Descricao,
                    Preco = p.Preco,
                    CodigoEAN = p.CodigoEAN,
                    QuantidadeDisponivel = p.QuantidadeDisponivel,
                    MarcaNome = p.Marca.NomeMarca,
                    CategoriaNome = p.Categoria.Nome,
                    ImagensUrls = p.Imagens.Select(p => p.Url).ToList()
                })
                .ToListAsync();

            if (productTable is null)
                return NotFound();
            else
                return Ok(productTable);
        }

        //[HttpPost("/adicionar-produto")]
        //public async Task<IActionResult> AddProduct(ProductModel productModel)
        //{
        //    var product = await _businessContext.Produtos.FirstOrDefaultAsync(p => p.Id.Equals(productModel.Id));

        //    if (product is not null)
        //        return BadRequest();

        //    var newProduct = new ProductModel();
        //    newProduct.Nome = productModel.Nome;
        //    newProduct.Descricao = productModel.Descricao;
        //    newProduct.Preco = productModel.Preco;
        //    newProduct.CodigoEAN = productModel.CodigoEAN;
        //    newProduct.QuantidadeDisponivel = productModel.QuantidadeDisponivel;
        //    newProduct.CategoriaNome = productModel.CategoriaNome;
        //    newProduct.MarcaNome = productModel.MarcaNome;
            

        //    _businessContext.Produtos.Add(newProduct);

        //    var result = await _businessContext.SaveChangesAsync();

        //    if (result.Equals(1))
        //        return Ok();
        //    return BadRequest();
        //}


        //[HttpGet("/produtos")]
        //public async Task<List<Produto>> GetProduto()
        //{
        //    return await _businessContext.Produtos.ToListAsync();
        //}
    }


}
