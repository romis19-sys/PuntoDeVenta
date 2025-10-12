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
        private string realText = ""; // Guardará el texto real
        public FrmPueba()
        {
            InitializeComponent();
            this.KeyPress += Utilidades.PasarFocus;
            this.KeyDown += Utilidades.ControlEsc;

            // Configurar el TextBox
            txtClave.PasswordChar = '*';
            txtClave.Multiline = true; // para permitir Enter
            txtClave.KeyDown += txtClave_KeyDown;
            txtClave.TextChanged += txtClave_TextChanged;
        }

        #region "Botones"
        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click_1(object sender, EventArgs e)
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
        #endregion

        #region "Eventos de las cajas de texto"
        private void txtClave_TextChanged(object sender, EventArgs e)
        {
            // Guardamos el texto real
            realText = txtClave.Text;
        }

        private void txtClave_KeyDown(object sender, KeyEventArgs e)
        {
            // Si se presiona Shift + Enter
            if (e.Shift && e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // evita salto de línea
                txtClave.PasswordChar = '\0'; // mostrar texto real
                txtClave.Text = realText;
                txtClave.SelectionStart = txtClave.Text.Length; // cursor al final
            }
            // Si se presiona solo Enter (sin Shift), ocultamos de nuevo
            else if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                txtClave.PasswordChar = '*'; // volver a ocultar
                txtClave.Text = realText;
                txtClave.SelectionStart = txtClave.Text.Length;
            }
        }
        #endregion
    }

}
