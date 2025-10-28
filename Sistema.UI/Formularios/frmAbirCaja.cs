using Farmacia.Entity;
using Sistema.BLL;
using Sistema.Entity;
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
    public partial class frmAbirCaja : Form
    {
        private Mensajes mensaje = new Mensajes();

        public frmAbirCaja(Boolean inicio)
        {
            InitializeComponent();

            this.KeyPress += Utilidades.PasarFocus;
            this.KeyDown += Utilidades.ControlEsc;
        }

        #region Métodos

        private void errorControl(string campo)
        {
            switch (campo)
            {
                case "idUsuario":
                    mensaje.mensajeError("el ID del usuario no es válido.");
                    break;

                case "numeroCaja":
                    errorIcono.SetError(txtNumeroCaja, "El número de caja inválido.");
                    txtNumeroCaja.Focus();
                    break;

                case "montoApertura":
                    errorIcono.SetError(txtMontoApertura, "El monto de apertura debe ser un número mayor a cero.");
                    txtMontoApertura.Focus();
                    break;
            }
        }

        private void buscarCaja()
        {
            try
            {
                var cajaAbierta = bCaja.buscarCaja(Variables.idUsuario);
                if(cajaAbierta.Rows.Count > 0)
                {
                    mensaje.mensajeInformacion("Ya existe una caja asociada a este usuario.");
                    Close();
                }
            }
            catch (Exception)
            {
                mensaje.mensajeError("Ocurrió un error al verificar el estado de la caja.");
            }
        }
        #endregion

        #region Botones de comando

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                errorIcono.Clear();

                if (!Int32.TryParse(txtNumeroCaja.Text.Trim(), out int numeroCaja))
                {
                    numeroCaja = 0;
                }

                if (!decimal.TryParse(txtMontoApertura.Text.Trim(), out decimal montoApertura))
                {
                    montoApertura = 0;
                }

                oCaja caja = new oCaja()
                {
                    idUsuario = Variables.idUsuario,
                    numeroCaja = numeroCaja,
                    montoApertura = montoApertura
                };

                resultadoOperacion resultado;

                resultado = bCaja.abirCaja(caja);

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
                Variables.idCaja = resultado.idCaja;
                Close();
            }
            catch (Exception)
            {
                mensaje.mensajeError("Error inesperado al aperturar la caja.");
            }
        }

        #endregion

        #region Cajas de texto
        private void txtNumeroCaja_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Utilidades.numerosEnteros(e, txtNumeroCaja.Text.Trim());
        }

        private void txtMontoApertura_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Utilidades.validarDecimal(sender, e);
        }

        private void txtNumeroCaja_Leave_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumeroCaja.Text.Trim()))
                txtNumeroCaja.Text = "0";
        }

        private void txtMontoApertura_Leave_1(object sender, EventArgs e)
        {
            Utilidades.formatoDecimal((Control)sender);
        }

        #endregion

        #region Eventos del Formulario
        private void frmAbirCaja_Load(object sender, EventArgs e)
        {
            buscarCaja();
        }




        #endregion

       
    }
}
