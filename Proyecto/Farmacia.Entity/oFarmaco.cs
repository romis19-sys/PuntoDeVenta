using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.Entity
{
    public class oFarmaco
    {
        public int idFarmaco { get; set; }
        public string codigo { get; set; }
        public string nombreFarmaco { get; set; } 
        public int idLaboratorio { get; set; }
        public string laboratorio { get; set; } // Para mostrar el nombre del laboratorio
        public int stock { get; set; }
        public string descripcion { get; set; }
        public int idPresentacion { get; set; }
        public string presentacion { get; set; } // Para mostrar el nombre de la presentación
        public string tipoVenta { get; set; }
        public DateTime? fechaCaducacion { get; set; }
        /*  public DateTime? fechaVencimiento { get; set; }*///si va 9 (es igual a la fecha de caducacion)
        public decimal precioCompra { get; set; }
        public decimal precioVenta { get; set; }
        public bool estado { get; set; }
        public DateTime fechaInsercion { get; set; }
        public DateTime? fechaModificacion { get; set; } // Nullable DateTime
    }
}
