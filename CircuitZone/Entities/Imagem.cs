using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitZone.Entities
{
    public class Imagem : BaseEntitity
    {
        public string Url { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public bool IsDeleted { get; set; }

    }
}
