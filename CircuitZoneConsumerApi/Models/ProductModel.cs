using CircuitZone.Entities;

namespace CircuitZoneConsumerApi.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Marca Marca { get; set; }
        public decimal Preco { get; set; }
        public string CodigoEAN { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public List<Imagem> Imagens { get; set; }
    }
}
