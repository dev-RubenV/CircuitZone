using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitZone.Entities
{
    public class Marca : BaseEntitity
    {
        public string NomeMarca { get; set; }
        public bool IsDeleted { get; set; }

    }
}
