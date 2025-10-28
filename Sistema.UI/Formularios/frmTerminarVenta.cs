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
    public partial class frmTerminarVenta : Form
    {
        Mensajes mensajes =  new Mensajes();

        public frmTerminarVenta( decimal totalGeneral)
        {
            InitializeComponent();

            this.KeyPress += Utilidades.PasarFocus;
            this.KeyDown += Utilidades.ControlEsc;

            txtTotalVenta.Text = totalGeneral.ToString("N2");
        }

        #region Métodos

        private bool ValidarCheckBox()
        {
            try
            {
                txtTotalVenta.Enabled = false;
                txtCambio.Enabled = false;
                if (!chkEfectivo.Checked && !chkTarjeta.Checked)
                {
                    mensajes.mensajeValidacion("Debe seleccionar al menos un método de pago.");
                    return false;
                }

                if (chkEfectivo.Checked && !chkTarjeta.Checked)
                {
                    txtEfectivo.ReadOnly = false;
                    txtTarjeta.ReadOnly = true;
                    //txtTarjeta.ForeColor = Color.Silver;
                    txtTarjeta.Enabled = false;
                    txtEfectivo.Enabled = true;

                    txtTarjeta.Text = "0.00";
                    txtEfectivo.Focus();

                    return true;
                }

                if (!chkEfectivo.Checked && chkTarjeta.Checked)
                {
                    txtEfectivo.ReadOnly = true;
                    txtTarjeta.ReadOnly = false;

                    txtTarjeta.Enabled = true;
                    txtEfectivo.Enabled = false;

                    txtEfectivo.Text = "0.00";
                    txtTarjeta.Focus();

                    return true;
                }

                if (chkEfectivo.Checked && chkTarjeta.Checked)
                {
                    txtEfectivo.ReadOnly = false;
                    txtTarjeta.ReadOnly = false;

                    txtTarjeta.Enabled = false;
                    txtEfectivo.Enabled = false;

                    txtEfectivo.Focus();

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                mensajes.mensajeError("Errros al validar método de pago.");
                return false;
            }
        }

        private bool CalcularCambio()
        {
            decimal pagoEfectivo = 0;
            decimal pagoTarjeta = 0;

            if (chkEfectivo.Checked)
            {
                if (!decimal.TryParse(txtEfectivo.Text.Trim(), out pagoEfectivo))
                {
                    mensajes.mensajeValidacion("El monto en efectivo no es válido.");
                    txtEfectivo.Focus();
                    return false;
                }
            }

            if (chkTarjeta.Checked)
            {
                if (!decimal.TryParse(txtTarjeta.Text.Trim(), out pagoTarjeta))
                {
                    mensajes.mensajeValidacion("El monto en tarjeta no es válido.");
                    txtTarjeta.Focus();
                    return false;
                }
            }

            if (!decimal.TryParse(txtTotalVenta.Text.Trim(), out decimal totalVenta))
            {
                mensajes.mensajeError("Error al obtener el total de la venta.");
                return false;
            }

            decimal totalPagado = pagoEfectivo + pagoTarjeta;
            decimal cambio = totalPagado - totalVenta;

            Variables.cambioVenta = cambio;

            txtCambio.Text = cambio.ToString("N2");
            return true;
        }

        #endregion

        #region Cajas de texto
        private void txtEfectivo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CalcularCambio();
            }
        }

        private void txtEfectivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidades.validarDecimal(sender, e);
        }

        private void tableLayoutPanel1_Leave(object sender, EventArgs e)
        {
            Utilidades.formatoDecimal((Control)sender);
        }

        private void txtTarjeta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CalcularCambio();
            }
        }

        private void txtTarjeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidades.validarDecimal(sender, e);
        }

        private void txtTarjeta_Leave(object sender, EventArgs e)
        {
            Utilidades.formatoDecimal((Control)sender);
        }
        #endregion

        #region eventos del formulario
        private void frmTerminarVenta_Load(object sender, EventArgs e)
        {
            ValidarCheckBox();
        }
        #endregion

        #region botones
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCheckBox())
                {
                    mensajes.mensajeValidacion("Debe seleccionar al menos un método de pago.");
                    return;
                }

                if (!CalcularCambio())
                    return;

                if(txtClientes.Text == string.Empty)
                {
                    mensajes.mensajeValidacion("Ingrese el nombre del cliente");
                    txtClientes.Focus();
                    return;
                }

                if (Variables.cambioVenta >= 0)
                {
                    Variables.terminarVentas = 1;
                    Variables.montoEfectivoVenta = Convert.ToDecimal(txtEfectivo.Text.Trim());
                    Variables.montoTarjetaVenta = Convert.ToDecimal(txtTarjeta.Text.Trim());
                    Variables.cambioVenta = Convert.ToDecimal(txtCambio.Text.Trim());
                    Variables.clientes = txtClientes.Text.Trim();
                    Close();
                }
                else
                {
                    mensajes.mensajeValidacion("El monto ingresado no cubre el total de la venta.");
                }
            }
            catch (Exception)
            {
                mensajes.mensajeError("Error al imprimir el voucher. Es probable que la venta no se complete.");
            }
        }


        #endregion

        private void chkEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            ValidarCheckBox();
        }

        private void chkTarjeta_CheckedChanged(object sender, EventArgs e)
        {
            ValidarCheckBox();
        }
    }
}
