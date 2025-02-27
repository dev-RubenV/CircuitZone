using CircuitZone.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CircuitZoneConsumerApi.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor insira um nome.")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Por favor insira uma descrição.")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Por favor insira um preço.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Por favor insira um código EAN.")]
        public string CodigoEAN { get; set; }

        [Required(ErrorMessage = "Por favor insira a quantidade dísponivel.")]
        public int QuantidadeDisponivel { get; set; }
        public string? MarcaNome { get; set; }
        public string? CategoriaNome { get; set; }
        public List<string>? ImagensUrls { get; set; } = new List<string>();
        public bool IsDeleted { get; set; }
        public DateTime IsCreated { get; set; }
        public DateTime IsUpdated { get; set; }
        public int MarcaId { get; set; }
        public int CategoriaId { get; set; }
    }

    public class MarcasModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor insira o nome da marca.")]
        public string NomeMarca { get; set; }
    }

    public class CategoriasModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor insira o nome da categoria.")]
        public string CategoriaNome { get; set; }
        public int ProdutoCount { get; set; }
        public decimal PrecoMedio { get; set; }
    }
}