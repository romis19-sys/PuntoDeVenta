using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema.UI.Formularios;

namespace Sistema.UI.Modulos
{
    public class Mensajes
    {
        public void mensajeError(string mensaje)
        {
            FrmMensajes frm = new FrmMensajes(mensaje, "error");
            frm.ShowDialog();
        }

        public void mensajeOk(string mensaje)
        {
            FrmMensajes frm = new FrmMensajes(mensaje, "ok");
            frm.ShowDialog();
        }

        public void mensajeValidacion(string mensaje)
        {
            FrmMensajes frm = new FrmMensajes(mensaje, "warning");
            frm.ShowDialog();
        }

        public void mensajeInformacion(string mensaje)
        {
            FrmMensajes frm = new FrmMensajes(mensaje, "info");
            frm.ShowDialog();
        }

        public DialogResult mensajeConfirmacion(string mensaje)
        {
            FrmMensajes frm = new FrmMensajes(mensaje, "confirmar");
            return frm.ShowDialog();
        }
    }
}
