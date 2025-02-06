using CircuitZone.Entities;
using System.Text.Json.Serialization;

namespace CircuitZoneConsumerApi.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public string CodigoEAN { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public string? MarcaNome { get; set; }
        public string? CategoriaNome { get; set; }
        public List<string>? ImagensUrls { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime IsCreated { get; set; }
        public DateTime IsUpdated { get; set; }
        public int MarcaId { get; set; }
        public int CategoriaId { get; set; }
    }

    public class MarcasModel
    {
        public int Id { get; set; }
        public string NomeMarca { get; set; }
    }

    public class CategoriasModel
    {
        public int Id { get; set; }
        public string CategoriaNome { get; set; }
    }
}