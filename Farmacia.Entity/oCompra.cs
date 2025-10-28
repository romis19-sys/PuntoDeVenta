using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.Entity
{
    public class oCompra
    {
		public int idCompra { get; set; }
		public int idLaboratorio { get; set; }
		public int idUsuario { get; set; }
		public decimal subtotal { get; set; }
		public decimal impuesto { get; set; }
        public decimal totalGeneral { get; set; }
        public string NCompra { get; set; }
		public Boolean estado { get; set; }
		public int idCaja { get; set; }
		public DataTable detalles { get; set; }
        public decimal montoEfectivo { get; set; }
        public  decimal montoTarjeta { get; set; }
        public decimal cambio { get; set; }
    }
}