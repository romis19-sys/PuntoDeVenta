using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.Entity
{
    public class OUsuario
    {
        public int idUsuario {  get; set; }
        public string Nombreusuario { get; set; }
        public string Nombre { get; set; }
        public string apellido { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string clave { get; set; }
        public int idRol {  get; set; }
    }
}
