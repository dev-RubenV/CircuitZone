using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitZone.Shared.Models
{
    public class ProductModel
    {
        public long Id { get; set; }
        //
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Marca { get; set; }
        public string Categoria { get; set; }
        public decimal Preco { get; set; }
        public string CodigoEAN { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public string Imagem { get; set; }

    }
}

