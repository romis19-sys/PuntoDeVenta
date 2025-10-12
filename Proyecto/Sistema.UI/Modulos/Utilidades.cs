using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.UI.Modulos
{
    public class Utilidades
    {
        private static Mensajes mensaje = new Mensajes();

        public static void PasarFocus(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }
            }
            catch (Exception)
            {
                mensaje.mensajeError("Error al pasar el focus.");
            }
        }


       

        public static void ControlEsc(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyCode == Keys.Escape)
                {
                    if(sender is Form formulario)
                    {
                        formulario.Close();
                    }
                }
            }
            catch (Exception)
            {
                mensaje.mensajeError("Error en el método controlarEsc.");
            }
        }
    }
}
