using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.Entity
{
    public class oVentas
    {
        public int idVenta { get; set; }
        public int idUsuario { get; set; } = 2;
        public string cliente { get; set; }
        public string NVenta { get; set; }
        public decimal subTotal { get; set; }
        public decimal impuesto { get; set; }
        public decimal totalGeneral { get; set; }
        public decimal montoEfectivo { get; set; }
        public decimal montoTargeta { get; set; }
        public decimal cambio { get; set; }
        public DataTable detalles { get; set; }
    }
}
