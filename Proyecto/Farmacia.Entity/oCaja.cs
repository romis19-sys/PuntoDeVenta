using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entity
{
    public class oCaja
    {
        public int idCaja { get; set; }
        public  int idUsuario { get; set; }
        public int numeroCaja { get; set; }
        public decimal montoApertura { get; set; }
        public decimal totalEfectivo { get; set; }
        public decimal totalTarjeta { get; set; }
        public decimal totalCambio { get; set; }
        public decimal totalEntregado { get; set; }
        public string codigo { get; set; }
        public string observacion { get; set; }
    }
}


