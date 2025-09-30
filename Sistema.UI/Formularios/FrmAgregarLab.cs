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
    public partial class FrmAgregarLab : Form
    {
        private Mensajes mensaje = new Mensajes();
        public event Action registroAgregado;
        Boolean actualizarRegistro = false;

        public FrmAgregarLab()
        {
            InitializeComponent();
            this.KeyPress += Utilidades.PasarFocus;
            this.KeyDown += Utilidades.ControlEsc;
        }

        public FrmAgregarLab(int id, string laboratorio, string telefono, string contacto)
        {
            InitializeComponent();
            this.KeyPress += Utilidades.PasarFocus;
            this.KeyDown += Utilidades.ControlEsc;

            txtId.Text = id.ToString();
            txtNombreLaboratorio.Text = laboratorio;
            txtTelefono.Text = telefono;
            txtContacto.Text = contacto;
            actualizarRegistro = true;
        }

        #region metodos
        private void errorControl(string campo)
        {
            switch (campo)
            {
                case "Laboratorio":
                    errorIcon.SetError(txtNombreLaboratorio, "Este campo es obligatorio.");
                    txtNombreLaboratorio.Focus();
                    break;
                case "Telefono":
                    errorIcon.SetError(txtTelefono, "Este campo es obligatorio.");
                    txtTelefono.Focus();
                    break;
                case "Contacto":
                    errorIcon.SetError(txtContacto, "Este campo es obligatorio.");
                    txtContacto.Focus();
                    break;
            }
        }

        private void limpiarControles()
        {
            txtId.Text = "0";
            txtNombreLaboratorio.Clear();
            txtTelefono.Clear();
            txtContacto.Clear();
            txtNombreLaboratorio.Focus();
        }
        #endregion

        #region botones de comando
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                errorIcon.Clear();
                oLaboratorio laboratorio = new oLaboratorio()
                {
                    laboratorio = txtNombreLaboratorio.Text.Trim(),
                    telefono = txtTelefono.Text.Trim(),
                    contacto = txtContacto.Text.Trim()
                };
                resultadoOperacion resultado;

                if(int.TryParse(txtId.Text.Trim(), out int Id) && Id == 0)
                {
                    resultado = bLaboratorio.registrarLaboratorio(laboratorio);
                }
                else
                {
                    laboratorio.idLaboratorio = Id;
                    resultado = bLaboratorio.editarLaboratorio(laboratorio);
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
                registroAgregado.Invoke();
                limpiarControles();
                if (actualizarRegistro) Close();
            }
            catch (Exception)
            {
                mensaje.mensajeError("Ocurrió un error inesperado.");
            }
            #endregion
        }
    }
}
