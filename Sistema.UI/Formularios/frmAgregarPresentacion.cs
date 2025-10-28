using Farmacia.BLL;
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
    public partial class frmAgregarPresentacion : Form
    {
        private Mensajes mensaje = new Mensajes();
        public event Action registroAgregado;
        Boolean actualizarRegistro = false;
        public frmAgregarPresentacion()
        {
            InitializeComponent();

            
            this.KeyPress += Utilidades.PasarFocus;
            this.KeyDown += Utilidades.ControlEsc;
        }

        public frmAgregarPresentacion(int idPresentacion, string nombrePresentacion)
        {
            InitializeComponent();

            
            this.KeyPress += Utilidades.PasarFocus;
            this.KeyDown += Utilidades.ControlEsc;

            txtId.Text = idPresentacion.ToString();
            txtNombrePresentacion.Text = nombrePresentacion;
            //txtDescripcion.Text = descripcionCategoria;

            actualizarRegistro = true;
        }

        #region Métodos

        private void errorControl(string campo)
        {
            switch (campo)
            {
                case "nombrePresentacion":
                    errorIcono.SetError(txtNombrePresentacion, "Este campo es obligatorio.");
                    txtNombrePresentacion.Focus();
                    break;
                    //case "descripcionCategoria":
                    //    errorIcono.SetError(txtDescripcion, "Este campo es obligatorio.");
                    //    txtDescripcion.Focus();
                    //    break;
            }
        }

        private void LimpiarControles()
        {
            txtId.Text = "0";
            txtNombrePresentacion.Clear();
            //txtDescripcion.Clear();
            txtNombrePresentacion.Focus();
        }

        #endregion

        #region Botones de comando
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                errorIcono.Clear();

                oPresentacion presentacion = new oPresentacion()
                {
                    nombrePresentacion = txtNombrePresentacion.Text.Trim(),
                    //descripcionCategoria = txtDescripcion.Text.Trim()
                };

                resultadoOperacion resultado;

                if (int.TryParse(txtId.Text.Trim(), out int Id) && Id == 0)
                {
                    resultado = bPresentacion.registrarPresentacion(presentacion);
                }
                else
                {
                    presentacion.IdPresentacion = Id;
                    resultado = bPresentacion.editarPresentacion(presentacion);
                }

                if (!resultado.esValido)
                {
                    mensaje.mensajeValidacion(resultado.mensaje);

                    if (!string.IsNullOrWhiteSpace(resultado.campoInvalido))
                    {
                        errorControl(resultado.campoInvalido);
                    }

                    return;
                }

                mensaje.mensajeOk(resultado.mensaje);
                registroAgregado?.Invoke();
                LimpiarControles();
                if (actualizarRegistro) Close();
            }
            catch (Exception)
            {
                mensaje.mensajeError("Ocurrió un error inesperado.");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion


    }
}
