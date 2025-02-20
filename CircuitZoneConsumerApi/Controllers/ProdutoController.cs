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
                    MarcaId = p.Marca.Id,
                    CategoriaId = p.Categoria.Id,
                    IsCreated = p.IsCreated,
                    ImagensUrls = p.Imagens.Any() ? p.Imagens.Select(p => p.Url).ToList()
                                  : new List<string> { "/Images/no-image.png" }
                })
                .ToListAsync();

            if (productTable is null)
                return NotFound();
            else
                return Ok(productTable);
        }
        
        [HttpGet("/getproduto")]
        public async Task<IActionResult> GetSingleProduct(int id)
        {
            var product = await _businessContext.Produtos
            .Where(p => p.IsDeleted == false && p.Id.Equals(id))
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
                    MarcaId = p.Marca.Id,
                    CategoriaId = p.Categoria.Id,
                    IsCreated = p.IsCreated,
                    IsUpdated = p.IsUpdated,
                    ImagensUrls = p.Imagens.Select(p => p.Url).ToList(),
                })
                .FirstOrDefaultAsync();

            if (product is null)
                return NotFound();
            else
                return Ok(product);
        }

        [HttpPost("/adicionar-produto")]
        public async Task<IActionResult> AddProdutos(ProductModel productModel)
        {
            var produto = await _businessContext.Produtos.FirstOrDefaultAsync(t => t.Id.Equals(productModel.Id));
            var produtoExistente = await _businessContext.Produtos.FirstOrDefaultAsync(p => p.CodigoEAN.Equals(productModel.CodigoEAN));

            if (produto is not null)
                return BadRequest();

            if (productModel.QuantidadeDisponivel < 0)
                return BadRequest("Produto não pode ter stock negativo.");

            if (produtoExistente is not null)
                return BadRequest("Produto com o mesmo EAN já existe.");

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
            newProduto.Imagens = productModel.ImagensUrls
            .Where(url => !string.IsNullOrEmpty(url))
            .Select(url => new Imagem { Url = url })
            .ToList();

            _businessContext.Produtos.Add(newProduto);

            var result = await _businessContext.SaveChangesAsync();

            if (result.Equals(1))
                return Ok();

            return BadRequest();
        }

        [HttpPut("/editproduto")]
        public async Task<IActionResult> EditProduto(ProductModel productModel)
        {
            var produto = await _businessContext.Produtos.Include(p => p.Imagens).FirstOrDefaultAsync(t => t.Id.Equals(productModel.Id));

            if (produto is null)
                return BadRequest();

            if (produto.QuantidadeDisponivel < 0)
                return BadRequest("Produto não pode ter stock negativo.");

            produto.Nome = productModel.Nome;
            produto.Descricao = productModel.Descricao;
            produto.MarcaId = productModel.MarcaId;
            produto.CategoriaId = productModel.CategoriaId;
            produto.Preco = productModel.Preco;
            produto.CodigoEAN = productModel.CodigoEAN;
            produto.QuantidadeDisponivel = productModel.QuantidadeDisponivel;
            produto.IsUpdated = DateTime.Now;
            produto.Imagens.Clear();
            produto.Imagens = productModel.ImagensUrls
                .Where(url => !string.IsNullOrEmpty(url))
                .Select(url => new Imagem { Url = url })
                .ToList();

            var result = await _businessContext.SaveChangesAsync();

            if (result > 0)
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
            .Where(c => c.IsDeleted == false && c.Nome != null)
            .Select(c => new CategoriasModel
            {
                Id = c.Id,
                CategoriaNome = c.Nome,
                ProdutoCount = _businessContext.Produtos.Where(p => p.IsDeleted == false && p.Nome != null && c.Id == p.CategoriaId)
                                .Count(),
                PrecoMedio = 0,
            })
            .ToListAsync();

            foreach (var categoria  in categoriasTable)
            {
                var produtos = await _businessContext.Produtos.Where(p => p.IsDeleted == false && p.Nome != null && p.CategoriaId == categoria.Id && p.QuantidadeDisponivel > 0).ToListAsync();

                if (categoria.ProdutoCount > 0)
                {
                    decimal total = 0;
                    foreach (var produto in produtos)
                    {
                        total += produto.Preco;
                    }
                    categoria.PrecoMedio = total / categoria.ProdutoCount;
                }
                else
                {
                    categoria.PrecoMedio = 0;
                }
            }

            return Ok(categoriasTable);
        }

        [HttpDelete("/deleteproduto")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produto = await _businessContext.Produtos.FirstOrDefaultAsync(t => t.Id.Equals(id));

            if (produto is null)
                return BadRequest();

            produto.IsDeleted = !produto.IsDeleted;

            var result = await _businessContext.SaveChangesAsync();

            if (result.Equals(1))
                return Ok();

            return BadRequest();
        }
        
        [HttpGet("/deleted-produtos")]
        public async Task<IActionResult> GetDeletedProduct()
        {
            var productTable = await _businessContext.Produtos
                .Where(p => p.IsDeleted == true)
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
                    ImagensUrls = p.Imagens.Any() ? p.Imagens.Select(p => p.Url).ToList()
                                  : new List<string> { "/Images/no-image.png" }
                })
                .ToListAsync();

            if (productTable is null)
                return NotFound();
            else
                return Ok(productTable);
        }

        [HttpGet("/getsingledeletedproduto")]
        public async Task<IActionResult> GetSingleDeletedProduct(int id)
        {
            var product = await _businessContext.Produtos.FirstOrDefaultAsync(p => p.IsDeleted == true && p.Id.Equals(id));

            if (product is null)
                return NotFound();
            else
                return Ok(product);
        }
    }



}
