using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.Entity
{
    public class resultadoOperacion
    {
        public bool esValido { get; set; }
        public string mensaje { get; set; }
        public string campoInvalido { get; set; }

        public int idCaja { get; set; }

        public string numeroVenta { get; set; }

        public string numeroCompra { get; set; }
        public string supervisor { get; set; }
    }
}
