using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CircuitZone.Entities
{
    public class Produto : BaseEntitity
    {
            public string Nome { get; set; }
            public string Descricao { get; set; }
            public int MarcaId { get; set; }
            public Marca Marca { get; set; }
            public decimal Preco { get; set; }
            public string CodigoEAN { get; set; }
            public int QuantidadeDisponivel { get; set; }
            public int CategoriaId { get; set; }
            public Categoria Categoria { get; set; }
            public List<Imagem> Imagens {get; set;}
}
