using Farmacia.BLL;
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
    public partial class frmBuscarFarmacosVenta : Form
    {
        Mensajes mensajes = new Mensajes();

        public frmBuscarFarmacosVenta(tipoFormulario invocador)
        {
            InitializeComponent();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Close();
        }


        #region Métodos

        public enum tipoFormulario
        {
            venta,
            compra
        }

        public tipoFormulario formularioInvocador { get; set; }

        private void listarFarmacos()
        {
            try
            {
                if (formularioInvocador == tipoFormulario.venta)
                {
                    dgvListado.DataSource = bFarmaco.listarFarmacos();

                    dgvListado.Columns[0].Visible = false;
                    dgvListado.Columns[9].Visible = false;
                }
                else if (formularioInvocador == tipoFormulario.compra)
                {

                }

                if (dgvListado.Rows.Count > 0)
                {
                    panelVacio.Visible = false;
                    txtBuscarProductos.Enabled = true;
                }
                else
                {
                    panelVacio.Visible = true;
                    txtBuscarProductos.Enabled = false;
                }

                txtBuscarProductos.Focus();
            }

            catch (Exception)
            {
                mensajes.mensajeError("Error al cargar registros.");
            }
        }

        private void BuscarProducto(int filtro, string nombreProducto)
        {
            //try
            //{
                dgvListado.DataSource = bFarmaco.buscarFarmacoVenta(filtro, nombreProducto);
                if (dgvListado.Rows.Count > 0)
                {
                    panelVacio.Visible = false;
                }
                else
                {
                    panelVacio.Visible = true;
                }
            //}
            //catch (Exception)
            //{
            //    mensajes.mensajeError("Error al buscar registros.");
            //}
        }

        private bool SeleccionarRegistro()
        {
            try
            {
                if (dgvListado.SelectedRows.Count > 0 && dgvListado.CurrentRow != null)
                {
                    Variables.idFarmaco = Convert.ToInt32(dgvListado.CurrentRow.Cells[0].Value);
                    Variables.farmaco = dgvListado.CurrentRow.Cells[2].Value.ToString();
                    Variables.stock = Convert.ToInt32(dgvListado.CurrentRow.Cells[4].Value);
                    Variables.precioConpra = Convert.ToDecimal(dgvListado.CurrentRow.Cells[9].Value);
                    Variables.precioVenta = Convert.ToDecimal(dgvListado.CurrentRow.Cells[10].Value);
                    //Variables. = dgvListado.CurrentRow.Cells[7].Value.ToString();

                    return true;
                }
                else
                {
                    panelVacio.Visible = true;
                    return false;
                }
            }
            catch (Exception)
            {
                mensajes.mensajeError("Error al seleccionar el registro.");
                return false;
            }
        }

        #endregion

        private void frmBuscarFarmacosVenta_Load(object sender, EventArgs e)
        {
            listarFarmacos();
        }

        private void txtBuscarProductos_TextChanged(object sender, EventArgs e)
        {
            BuscarProducto(1, txtBuscarProductos.Text.Trim());
        }

        private void txtBuscarProductos_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    bool agregarRegistro = SeleccionarRegistro();

                    if (agregarRegistro)
                    {
                        if (formularioInvocador == tipoFormulario.venta)
                        {
                            frmVentas formularioPedido = this.Owner as frmVentas;
                            if (formularioPedido != null)
                            {
                                formularioPedido.AgregarProductos(Variables.idFarmaco, Variables.farmaco, Variables.precioVenta, Variables.stock);
                            }

                            e.Handled = true;
                            e.SuppressKeyPress = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                mensajes.mensajeError("Error inesperado al agregar registro.");
            }
        }

        private void dgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvListado.SelectedRows.Count >= 1)
            {
                KeyEventArgs keyEventArgs = new KeyEventArgs(Keys.Enter);
                txtBuscarProductos_KeyDown(this.txtBuscarProductos, keyEventArgs);
            }
        }
    }
}
