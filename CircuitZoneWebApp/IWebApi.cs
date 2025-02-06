using Refit;
using CircuitZoneConsumerApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CircuitZoneWebApp
{
    public interface IWebApi
    {
        [Get("/produtos")]
        Task<List<ProductModel>> GetProduto();

        [Get("/marcas")]
        Task<List<MarcasModel>> GetMarcas();

        [Get("/categorias")]
        Task<List<CategoriasModel>> GetCategorias();

        [Post("/adicionar-produto")]
        Task<HttpResponseMessage> AddProduto([FromBody] ProductModel productModel);

        [Delete("/deleteproduto")]
        Task<HttpResponseMessage> DeleteProduto(int id);
    }
}
