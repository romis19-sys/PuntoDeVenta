using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.UI.Modulos
{
    public class Variables
    {
        //Esta clase funciona para la parte de caja
        public static int idCaja { get; set; } = 0;
        public static int idUsuario { get; set; } = 2;
        public static string usuario { get; set; }


        //Esto fue que puspo Fret para la venta
        //public static int idUsuario { get; set; }
        //public static string usuario { get; set; }
        public static int terminarVentas { get; set; } = 0;
        public static decimal montoEfectivoVenta { get; set; } = 0;
        public static decimal montoTarjetaVenta { get; set; } = 0;
        public static decimal cambioVenta { get; set; } = 0;
        public static string numeroPedido { get; set; }
        public static int idFarmaco { get; set; }
        public static string farmaco { get; set; }
        public static int cantidadVenta { get; set; } //este no lo e encontrado
        public static decimal precioConpra { get; set; } = 0;
        public static decimal precioVenta { get; set; } = 0;
        public static int stock { get; set; }
        public static string NVentas { get; set; }
        public static string clientes { get; set; }


        //Rodolfo
        
        public static int terminarCompra { get; set; } = 0;
        public static decimal montoEfectivo { get; set; } = 0;
        public static decimal montoTarjeta { get; set; } = 0;
        public static decimal cambio { get; set; } = 0;
        public static string numeroCompra { get; set; }
        public static string NCompra { get; set; }

        //public static int idFarmaco { get; set; }
        public static int idLaboratorio { get; set; }
        public static int cantidad { get; set; }
        //public static string farmaco { get; set; }
        public static decimal precio { get; set; } = 0;
        public static decimal impuesto { get; set; }
    }
}
