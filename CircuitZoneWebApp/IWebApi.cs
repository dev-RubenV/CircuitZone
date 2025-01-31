using Refit;
using CircuitZoneConsumerApi.Models;

namespace CircuitZoneWebApp
{
    public interface IWebApi
    {
        [Get("/produtos")]
        Task<List<ProductModel>> GetProduto();
    }
}
