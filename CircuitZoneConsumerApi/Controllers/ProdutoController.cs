using CircuitZone.Data;
using CircuitZone.Entities;
using CircuitZoneConsumerApi.Models;
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
        //

        [HttpPost("/adicionar-produto")]
        public async Task<IActionResult> AddProdutos(ProductModel productModel)
        {
            var produto = await _businessContext.Produtos.FirstOrDefaultAsync(t => t.Id.Equals(productModel.Id));

            if (produto is not null)
                return BadRequest();

            var newProduto = new Produto();
            newProduto.Nome = productModel.Nome;
            newProduto.Descricao = productModel.Descricao;
            newProduto.MarcaId = productModel.MarcaId;
            newProduto.CategoriaId = productModel.CategoriaId;
            newProduto.Preco = productModel.Preco;
            newProduto.CodigoEAN = productModel.CodigoEAN;
            newProduto.QuantidadeDisponivel = productModel.QuantidadeDisponivel;
            newProduto.IsDeleted = false;
            newProduto.IsCreated = DateTime.Now;
            //newProduto.Imagens = productModel.ImagensUrls;

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
            produto.MarcaId = productModel.MarcaId;
            produto.CategoriaId = productModel.CategoriaId;
            produto.Preco = productModel.Preco;
            produto.CodigoEAN = productModel.CodigoEAN;
            produto.QuantidadeDisponivel = productModel.QuantidadeDisponivel;
            produto.IsDeleted = productModel.IsDeleted;
            produto.IsUpdated = DateTime.Now;
            //produto.Imagem = productModel.Imagem;

            var result = await _businessContext.SaveChangesAsync();

            if (result.Equals(1))
                return Ok();

            return BadRequest();
        }

        [HttpGet("/marcas")]
        public async Task<IActionResult> GetMarcas()
        {
            var marcasTable = await _businessContext.Marcas
            .Where(m => m.IsDeleted == false && m.NomeMarca != null)
            .Select(m => new MarcasModel
            {
                Id = m.Id,
                NomeMarca = m.NomeMarca,
            })
            .ToListAsync();

            return Ok(marcasTable);
        }

        [HttpGet("/categorias")]
        public async Task<IActionResult> GetCategorias()
        {
            var categoriasTable = await _businessContext.Categorias
            .Where(m => m.IsDeleted == false && m.Nome != null)
            .Select(m => new CategoriasModel
            {
                Id = m.Id,
                CategoriaNome = m.Nome,
            })
            .ToListAsync();

            return Ok(categoriasTable);
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
