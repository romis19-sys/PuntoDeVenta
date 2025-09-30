using Farmacia.DAL;
using Sistema.UI.Formularios;
using Sistema.UI.Modulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.UI
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var mensaje = new Mensajes();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //var datosconexion = GestorConexion.CargarDatosConexion();

            //if (string.IsNullOrWhiteSpace(datosconexion.servidor) ||
            //   string.IsNullOrWhiteSpace(datosconexion.baseDatos) ||
            //   string.IsNullOrWhiteSpace(datosconexion.usuario) ||
            //   string.IsNullOrWhiteSpace(datosconexion.clave))
            //{
            //    using (var frm = new frmConexion())
            //    {
            //        if (frm.ShowDialog() != DialogResult.OK)
            //        {
            //            mensaje.mensajeError("No se configuró la conexión. La aplicación se cerrará.");
            //            return;
            //        }
                    
            //    }
            //}

            Application.Run(new MDIMenu());
        }
    }
}
