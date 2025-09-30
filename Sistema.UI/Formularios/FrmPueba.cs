using Farmacia.DAL;
using Farmacia.Entity;
using Sistema.UI.Modulos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.UI.Formularios
{
    public partial class FrmPueba : Form
    {
        private Mensajes mensajes = new Mensajes();
        public FrmPueba()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            errorIcono.Clear();
            bool datosValidos = true;

            var CajasTextos = new List<Control> { txtBd, txtClave, txtServidor, txtUsuario };

            foreach (Control control in CajasTextos)
            {
                if (control is Guna.UI2.WinForms.Guna2TextBox textBoxt)
                {
                    if (string.IsNullOrWhiteSpace(textBoxt.Text))
                    {
                        errorIcono.SetError(textBoxt, "Este campo es obligatorio.");
                        datosValidos = false;
                    }
                }
            }


            if (!datosValidos)
            {
                mensajes.mensajeValidacion("Información incompleta. Verifique por favor.");
                return;
            }

            var datosConexion = new DatosConexion
            {
                servidor = txtServidor.Text.Trim(),
                baseDatos = txtBd.Text.Trim(),
                usuario = txtUsuario.Text.Trim(),
                clave = txtClave.Text.Trim()
            };

            try
            {
                if (GestorConexion.ProbarConexion(datosConexion, out string error))
                {
                    GestorConexion.GuardarConexion(datosConexion);
                    mensajes.mensajeOk("Datos de conexión guardados con éxito.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    mensajes.mensajeError("Error en la conexión con el servidor.");
                }
            }
            catch (Exception)
            {
                mensajes.mensajeError("Error al guardar los datos de conexión.");
            }
        }
    }
    
}
