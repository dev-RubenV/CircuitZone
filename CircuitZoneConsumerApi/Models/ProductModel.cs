using CircuitZone.Entities;

namespace CircuitZoneConsumerApi.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public string CodigoEAN { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public string MarcaNome { get; set; }
        public string CategoriaNome { get; set; }
        public List<string> ImagensUrls { get; set; }
    }
}