using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.Entity
{
    public class DatosConexion
    {
        public string servidor { get; set; }
        public string baseDatos { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }

        public string CadenaConexion =>
            $"Data Source={servidor};Initial Catalog={baseDatos};User ID={usuario};Password={clave};Encrypt=False";

    }
}
