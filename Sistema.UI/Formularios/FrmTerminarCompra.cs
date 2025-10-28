using Farmacia.BLL;
using Sistema.UI.Modulos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.UI.Formularios
{
    public partial class FrmTerminarCompra : Form
    {
        private Mensajes mensajes = new Mensajes(); 
        public FrmTerminarCompra(decimal totalGeneral)
        {
            InitializeComponent();

            this.KeyPress += Utilidades.PasarFocus;
            this.KeyDown += Utilidades.ControlEsc;

            txtTotalVenta.Text = totalGeneral.ToString("N2");
        }

        #region "Metodos"
        private void CargarProveedores()
        {
            try
            {
                var proveedores = bCompras.CargarProveedores();
                if (proveedores != null && proveedores.Rows.Count > 0)
                {
                    cboProveedores.DataSource = proveedores;
                    cboProveedores.ValueMember = "ID";
                    cboProveedores.DisplayMember = "Laboratorio";
                    cboProveedores.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private bool validarCheckBox()
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
                mensajes.mensajeError("Ocurrió un error al validar método de pago.");
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
                mensajes.mensajeError("Error al obtener el total de la compra.");
                return false;
            }

            decimal totalPagado = pagoEfectivo + pagoTarjeta;
            decimal cambio = totalPagado - totalVenta;

            Variables.cambio = cambio;

            txtCambio.Text = cambio.ToString("N2");
            return true;
        }
        #endregion

        #region "Botones"
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validarCheckBox())
                {
                    mensajes.mensajeValidacion("Debe seleccionar al menos un método de pago.");
                    return;
                }

                if (!CalcularCambio())
                    return;

                if (cboProveedores.Text == string.Empty)
                {
                    mensajes.mensajeValidacion("Ingrese un laboratorio.");
                    cboProveedores.Focus();
                    return;
                }

                if (Variables.cambio >= 0)
                {
                    Variables.terminarCompra = 1;
                    Variables.montoEfectivo = Convert.ToDecimal(txtEfectivo.Text.Trim());
                    Variables.montoTarjeta = Convert.ToDecimal(txtTarjeta.Text.Trim());
                    Variables.cambio = Convert.ToDecimal(txtCambio.Text.Trim());
                    Variables.idLaboratorio = Convert.ToInt32(cboProveedores.SelectedValue);
                    Variables.NCompra = txtNCompra.Text.Trim();
                    Close();
                }
                else
                {
                    mensajes.mensajeValidacion("El monto ingresado no cubre el total de la compra.");
                }
            }
            catch (Exception)
            {
                mensajes.mensajeError("Error al imprimir el voucher. Es probable que la compra no se complete.");
            }
        }
        #endregion

        #region "Eventos del Formulario"
        private void FrmTerminarCompra_Load(object sender, EventArgs e)
        {
            CargarProveedores();
            validarCheckBox();
        }
        #endregion

        #region "Cajas de Texto"
        private void chkEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            validarCheckBox();
        }

        private void chkTarjeta_CheckedChanged(object sender, EventArgs e)
        {
            validarCheckBox();
        }
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

        private void txtEfectivo_Leave(object sender, EventArgs e)
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
    }
}
