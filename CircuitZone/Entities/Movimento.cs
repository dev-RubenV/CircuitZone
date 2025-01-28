using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitZone.Entities
{
    public class Movimento : BaseEntitity
    {
        public int Quantidade { get; set; }
        public DateTime DataMovimento { get; set; }
        public int TipoMovimentoId { get; set; }
        public TipoMovimento TipoMovimento { get; set; }
        public int UtilizadorId { get; set; }
        public Utilizador Utilizador { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
    }
}
